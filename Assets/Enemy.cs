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
                //если луч не попал в цель
                if (hit.collider.gameObject != _player1.gameObject)
                {
                    Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
                }
                //если луч попал в цель
                else
                {
                    Debug.Log("Попадаю во врага!!!");
                }
                //просто для наглядности рисуем луч в окне Scene
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