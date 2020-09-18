using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace unit
{
    public class PlayerSearch : MonoBehaviour
    {
        private bool isPlayer1 = false;
        private bool isPlayer2 = false;
        protected int ResultSearch()
        {

            return 1;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            switch(other.tag)
            {
                case "Player1":
                    isPlayer1 = true;
                    break;
                case "Player2":
                    isPlayer2 = true;
                    break;
            }
            Debug.Log(isPlayer1);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Player1":
                    isPlayer1 = false;
                    break;
                case "Player2":
                    isPlayer2 = false;
                    break;
            }
            Debug.Log(isPlayer1);
        }
    }
}

