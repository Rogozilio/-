using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using asd;

public class player : MonoBehaviour
{
    public Rigidbody2D player1;
    private SpriteRenderer sprite;
    public float move;
    public float speed = 10f;
    private Animator ani;
    private double angle;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");
        angle = ControlOptions.GetAngle();
    }

    void Update()
    {
        player1.velocity = new Vector2(move * speed, player1.velocity.y);
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if(!double.IsNaN(angle) && ani.GetInteger("statusFight") == 1)
        {
            ani.SetInteger("statusFight", 2);
        }
        if (double.IsNaN(angle) && ani.GetInteger("statusFight") == 2)
        {
            ani.SetInteger("statusFight", 1);
        }
        if (Input.GetButtonDown("Y"))
        {
            if(ani.GetInteger("statusFight") == 0)
            {
                ani.SetInteger("statusFight", 1);
            }
            else
            {
                ani.SetInteger("statusFight", 0);
            } 
        }
        Walk();
        Fight();
        Input.GetAxis("Horizontal");
    }
    void Walk()
    {
        if (move > 0)
        {
            sprite.flipX = false;
        }
        if (move < 0)
        {
            sprite.flipX = true;
        }
        if (move == 0)
        {
            ani.SetBool("isWalk", false);
        }
        else
        {
            ani.SetBool("isWalk", true);
        }
    }
    void Fight()
    {
        ani.SetFloat("AngleStick", (float)angle);
    }
}