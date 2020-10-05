using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D player1;
    private SpriteRenderer sprite;
    private float _move;
    public float speed = 10f;
    private Animator _ani;
    private double _angle;
    public double Angle
    {
        get
        {
            return _angle;
        }
    }
    public double Move {
        get
        {
            return _move;
        } 
    }

    void Start()
    {
        _ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _move = Input.GetAxis("Horizontal");
        _angle = ControlOptions.GetAngle();
    }

    void Update()
    {
        player1.velocity = new Vector2(_move * speed, player1.velocity.y);
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScreen");
        }
        if (!double.IsNaN(_angle) && _ani.GetInteger("statusFight") == 1)
        {
            _ani.SetInteger("statusFight", 2);
        }
        if (double.IsNaN(_angle) && _ani.GetInteger("statusFight") == 2)
        {
            _ani.SetInteger("statusFight", 1);
        }
        if (Input.GetButtonDown("Y"))
        {
            if (_ani.GetInteger("statusFight") == 0)
            {
                _ani.SetInteger("statusFight", 1);
            }
            else
            {
                _ani.SetInteger("statusFight", 0);
            }
        }
        Walk();
        Fight();
    }
    void Walk()
    {
        if (_move > 0)
        {
            sprite.flipX = false;
        }
        if (_move < 0)
        {
            sprite.flipX = true;
        }
        if (_move == 0)
        {
            _ani.SetBool("isWalk", false);
        }
        else
        {
            _ani.SetBool("isWalk", true);
        }
    }
    void Fight()
    {
        _ani.SetFloat("AngleStick", (float)_angle);
    }
}