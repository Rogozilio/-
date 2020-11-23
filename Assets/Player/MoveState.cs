using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class MoveState : State
{
    private Player          _player;
    private float           _moveX;
    private float           _speed;
    private Vector2         _velocity;
    public MoveState(Player player, StateMachine movementSM)
        : base(movementSM)
    {
        _player = player;
        _movementSM = movementSM;
    }

    public override void Enter()
    {
        base.Enter();
        _speed = _player.speed;
        _player.ani.SetBool("isWalk", true);
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _moveX = _player.Move;
        _velocity = _player.rigidBody.velocity;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_moveX > 0)
        {
            _player.sprite.flipX = false;
        }
        else if (_moveX < 0)
        {
            _player.sprite.flipX = true;
        }
        else
        {
            _movementSM.ChangeState(_player.idleS);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _player.rigidBody.velocity = new Vector2(_moveX * _speed, _velocity.y);
    }
}
