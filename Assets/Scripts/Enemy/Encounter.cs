using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Enemy
{
    struct Encounter
    {
        private GameObject      _player;
        private bool            _isPlayer;
        private bool            _isPlayerBehind;
        private GameObject      _enemy;
        private Ray             _focus;
        private RaycastHit2D    _block;
        public GameObject       player => _player;
        public bool             isPlayer => _isPlayer;
        public bool             isPlayerBehind => _isPlayerBehind;
        public Ray              focus => _focus;
        public Vector3          point => _player.transform.TransformDirection(focus.direction) * block.distance;
        public RaycastHit2D     block => _block;

        public Encounter(GameObject enemy, GameObject player)
        {
            _isPlayerBehind = false;
            _isPlayer = true;
            _enemy = enemy;
            _player = player;
            _focus = new Ray(enemy.transform.position, player.transform.position - enemy.transform.position);
            _block = Physics2D.Raycast(_focus.origin, _focus.direction);
        }
        private bool IsPlayerBehind()
        {
            if((focus.direction.x > 0 
                && _enemy.transform.localScale.x > 0)
                || (focus.direction.x < 0
                && _enemy.transform.localScale.x < 0))
            {
                return true;
            }
            return false;
        }
        public void Active()
        {
            if (_isPlayer)
            {
                _focus = new Ray(_enemy.transform.position, 
                    player.transform.position - _enemy.transform.position);
                _block = Physics2D.Raycast(_focus.origin, _focus.direction);

                if(_block.collider.gameObject.tag != _player.tag)
                {
                    _isPlayer = false;
                }
                _isPlayerBehind = IsPlayerBehind();
            }
        }
    }
}
