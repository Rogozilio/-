using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace asd
{
    public class infoLog : MonoBehaviour
    {
        public Rigidbody2D player1;
        private Text _textLog;
        // Start is called before the first frame update
        void Start()
        {
            _textLog = GetComponent<Text>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            double cos = ControlOptions.GetAngle();
            _textLog.text = "GetAxis:\n"
                + "PlayerSpeed = " + player1.velocity.x + "\n"
                + "Move = " + Input.GetAxis("Horizontal") + "\n"
                + "RightStickX = " + Input.GetAxis("RightStickX") + "\n"
                + "RightStickY = " + Input.GetAxis("RightStickY") + "\n"               
                + "angel = " + cos + "\n";
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                _textLog.enabled = !_textLog.enabled;
            }
        }
    }
}
