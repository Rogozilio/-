using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;
using AI;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Encounter       _player1;
        private Encounter       _player2;
        /// <summary>
        /// Значение статуса обнаружения играков
        /// </summary>
        private StatusSearch    statusSearch;
        public Animator         ani;
        public SpriteRenderer   sprite;

        public NodeSequence    Nroot;
        public NodeSelector     Nanimation;

        public NodeAction       Npeaceful;
        public NodeAction       Nattack1;
        public NodeAction       Nattack2;
        public NodeAction       NattackAndDefend;
        public NodeAction       Nreversal;
        public NodeAction       Ndrawray;

        private NodeStates DrawRay()
        {
            if (_player1.isPlayer == true)
            {
                _player1.Active();
                Debug.DrawRay(_player1.focus.origin, _player1.point, Color.green);
            }
            if (_player2.isPlayer == true)
            {
                _player2.Active();
                Debug.DrawRay(_player2.focus.origin, _player2.point, Color.green);
            }
            if (_player1.isPlayer == false && _player2.isPlayer == false)
                return NodeStates.FAILURE;
            else
                return NodeStates.SUCCESS;
        }
        private NodeStates WithoutOpponent()
        {
            if (statusSearch == StatusSearch.Nothing
                && ani.GetInteger("searchStatus") != 0)
            {
                ani.SetInteger("searchStatus", 0);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        private NodeStates OneOpponent()
        {
            if (statusSearch == StatusSearch.One
                && ani.GetInteger("searchStatus") != 1)
            {
                ani.SetInteger("searchStatus", 1);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        private NodeStates TwoOpponent()
        {
            if (statusSearch == StatusSearch.Two
                && ani.GetInteger("searchStatus") != 2)
            {
                ani.SetInteger("searchStatus", 2);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        private NodeStates OneOpponentPlusOne()
        {
            if (statusSearch == StatusSearch.OneWithoutSecond
                && ani.GetInteger("searchStatus") != 3)
            {
                ani.SetInteger("searchStatus", 3);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        private NodeStates Reversal()
        {
            if ((_player1.isPlayerBehind && _player2.isPlayerBehind && _player1.isPlayer && _player2.isPlayer)
                || (_player1.isPlayerBehind && !_player2.isPlayerBehind && _player1.isPlayer && !_player2.isPlayer)
                || (!_player1.isPlayerBehind && _player2.isPlayerBehind && !_player1.isPlayer && _player2.isPlayer))
            {
                transform.localScale =
                    new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                ani.SetInteger("searchStatus", 1);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        void Start()
        {
            ani = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            ani.SetInteger("searchStatus", 0);

            List<Node> rootChildren = new List<Node>();
            List<Node> animationChildren = new List<Node>();

            Npeaceful = new NodeAction(WithoutOpponent);
            Nattack1 = new NodeAction(OneOpponent);
            Nattack2 = new NodeAction(TwoOpponent);
            NattackAndDefend = new NodeAction(OneOpponentPlusOne);
            Nreversal = new NodeAction(Reversal);
            Ndrawray = new NodeAction(DrawRay);

            animationChildren.Add(Nreversal);
            animationChildren.Add(Npeaceful);
            animationChildren.Add(Nattack1);
            animationChildren.Add(Nattack2);
            animationChildren.Add(NattackAndDefend);

            Nanimation = new NodeSelector(animationChildren);

            rootChildren.Add(Ndrawray);
            rootChildren.Add(Nanimation);

            Nroot = new NodeSequence(rootChildren);
        }
        void Update()
        {
            SearchPlayer();
            Nroot.Evaluate();
            Debug.Log(Ndrawray.nodeState);
        }
        private StatusSearch SearchPlayer()
        {
            if (!_player1.isPlayer && !_player2.isPlayer)
            {
                statusSearch = StatusSearch.Nothing;
            }
            else if (_player1.isPlayer && !_player2.isPlayer ||
                (!_player1.isPlayer && _player2.isPlayer))
            {
                statusSearch = StatusSearch.One;
            }
            else if ((_player1.isPlayer && _player2.isPlayer) 
                && ((_player1.isPlayerBehind && !_player2.isPlayerBehind) 
                || (!_player1.isPlayerBehind && _player2.isPlayerBehind)))
            {
                statusSearch = StatusSearch.OneWithoutSecond;
            }
            else
            {
                statusSearch = StatusSearch.Two;
            }
            Debug.Log(statusSearch);
            return statusSearch;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.name)
            {
                case "Player1":
                _player1 = new Encounter(this.gameObject, other.gameObject);
                break;
                case "Player2":
                _player2 = new Encounter(this.gameObject, other.gameObject);
                break;
            }
        }

        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    switch (other.name)
        //    {
        //        case "Player1":
        //        _isPlayer1 = false;
        //        break;
        //        case "Player2":
        //        _isPlayer2 = false;
        //        break;
        //    }
        //}
    }
}