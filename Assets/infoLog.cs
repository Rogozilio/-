using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoLog : MonoBehaviour
{
    private Player _player1;
    private Text _textLog;
    void Start()
    {
        _textLog = GetComponent<Text>();
        _player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
    }
    void FixedUpdate()
    {
        if(_player1 != null)
        {
            //_textLog.text = AI.Data.Player(_player1);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            _textLog.enabled = !_textLog.enabled;
        }
    }
}