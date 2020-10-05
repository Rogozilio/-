using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;
using AI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ����� 1 � ���� ������?
    /// </summary>
    private bool            _isPlayer1 = false;
    /// <summary>
    /// ����� 2 � ���� ������?
    /// </summary>
    private bool            _isPlayer2 = false;
    private GameObject      _player1;
    private GameObject      _player2;
    /// <summary>
    /// �������� ������� ����������� �������
    /// </summary>
    private StatusSearch statusSearch;

    void Update()
    {
        if (_isPlayer1 == true)
        {
            RaycastHit hit;

            Ray ray = Distance.Ray(transform.position,
                _player1.transform.position - transform.position);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                //���� ��� �� ����� � ����
                if (hit.collider.gameObject != _player1.gameObject)
                {
                    Debug.Log("���� � ����� ����������� ������: " + hit.collider.name);
                }
                //���� ��� ����� � ����
                else
                {
                    Debug.Log("������� �� �����!!!");
                }
                //������ ��� ����������� ������ ��� � ���� Scene
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }

        }
    }
    protected StatusSearch SearchPlayer()
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