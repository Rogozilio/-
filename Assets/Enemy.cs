using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;
using AI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Игрок 1 в поле зрения?
    /// </summary>
    private bool            _isPlayer1 = false;
    /// <summary>
    /// Игрок 2 в поле зрения?
    /// </summary>
    private bool            _isPlayer2 = false;
    private GameObject      _player1;
    private GameObject      _player2;
    /// <summary>
    /// Значение статуса обнаружения играков
    /// </summary>
    private StatusSearch    statusSearch;
    public Animator         ani;

    public NodeSelector     rootNode;
    public NodeAction       node1;
    public NodeAction       node2;
    public NodeAction       node3;
    public NodeAction       node4;

    private NodeStates WithoutOpponent()
    {
        if (statusSearch == StatusSearch.Nothing)
        {
            ani.SetInteger("searcheStatus", 0);
            Debug.Log("Nothing");
            return NodeStates.SUCCESS;
        }
        return NodeStates.FAILURE;
    }
    private NodeStates OneOpponent()
    {
        if(statusSearch == StatusSearch.One)
        {
            ani.SetInteger("searcheStatus", 1);
            Debug.Log("One");
            return NodeStates.SUCCESS;
        }
        return NodeStates.FAILURE;
    }
    private NodeStates TwoOpponent()
    {
        if (statusSearch == StatusSearch.Two)
        {
            ani.SetInteger("searcheStatus", 2);
            Debug.Log("Two");
            return NodeStates.SUCCESS;
        }
        return NodeStates.FAILURE;
    }
    private NodeStates OneOpponentPlusOne()
    {
        if (statusSearch == StatusSearch.OneWithoutSecond)
        {
            ani.SetInteger("searcheStatus", 1);
            Debug.Log("OnePlusOne");
            return NodeStates.SUCCESS;
        }
        return NodeStates.FAILURE;
    }
    void Start()
    {
        ani = GetComponent<Animator>();

        List<Node> rootChildren = new List<Node>();

        node1 = new NodeAction(WithoutOpponent);
        node2 = new NodeAction(OneOpponent);
        node3 = new NodeAction(TwoOpponent);
        node4 = new NodeAction(OneOpponentPlusOne);

        
        rootChildren.Add(node1);
        rootChildren.Add(node2);
        rootChildren.Add(node3);
        rootChildren.Add(node4);

        rootNode = new NodeSelector(rootChildren);
    }
    void Update()
    {
        SearchPlayer();
        rootNode.Evaluate();
           
    }
    private StatusSearch SearchPlayer()
    {
        if (_isPlayer1 == false && _isPlayer2 == false)
        {
            statusSearch = StatusSearch.Nothing;
        }
        else if ((_isPlayer1 == true && _isPlayer2 == false) ||
            (_isPlayer1 == false && _isPlayer2 == true))
        {
            statusSearch = StatusSearch.One;
        }
        else
        {
            statusSearch = StatusSearch.Two;
        }
        return statusSearch;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player1":
            _isPlayer1 = true;
            _player1 = other.gameObject;
            break;
            case "Player2":
            _isPlayer2 = true;
            _player2 = other.gameObject;
            break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player1":
            _isPlayer1 = false;
            break;
            case "Player2":
            _isPlayer2 = false;
            break;
        }
    }
}