using AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private double          _angle;
    private float           _move;
    private bool            _enable = true;

    public Rigidbody2D      rigidBody;
    public SpriteRenderer   sprite;
    public float            speed = 10f;
    public Animator         ani;

    public StateMachine     movementSM;
    public IdleState        idleS;
    public MoveState        moveS;
    public double Angle
    {
        get
        {
            return _angle;
        }
    }
    public float Move {
        get
        {
            return _move;
        } 
    }

    void Start()
    {
        ani         = GetComponent<Animator>();
        sprite      = GetComponent<SpriteRenderer>();
        rigidBody   = GetComponent<Rigidbody2D>();

        movementSM  = new StateMachine();

        idleS       = new IdleState(this, movementSM);
        moveS       = new MoveState(this, movementSM);
        movementSM.Initialize(idleS);
    }

    void Update()
    {
        if(_enable)
        {
            movementSM.CurrentState.HandleInput();
            movementSM.CurrentState.LogicUpdate();
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _enable = true;
            if(gameObject.name == "Player2")
            {
                _enable = false;
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            _enable = true;
            if (gameObject.name == "Player1")
            {
                _enable = false;
            }
        }
        if (!double.IsNaN(_angle) && ani.GetInteger("statusFight") == 1)
        {
            ani.SetInteger("statusFight", 2);
        }
        if (double.IsNaN(_angle) && ani.GetInteger("statusFight") == 2)
        {
            ani.SetInteger("statusFight", 1);
        }
        if (Input.GetButtonDown("Y"))
        {
            if (ani.GetInteger("statusFight") == 0)
            {
                ani.SetInteger("statusFight", 1);
            }
            else
            {
                ani.SetInteger("statusFight", 0);
            }
        }
        Fight();
    }
    private void FixedUpdate()
    {
        if (_enable)
        {
            movementSM.CurrentState.PhysicsUpdate();

            if (Input.GetKey(KeyCode.Space))
            {
                rigidBody.AddForce(transform.up * 0.5f, ForceMode2D.Impulse);
            }
        }
        _move = Input.GetAxis("Horizontal");
        _angle = ControlOptions.GetAngle();
        
    }
    void Fight()
    {
        ani.SetFloat("AngleStick", (float)_angle);
    }
}