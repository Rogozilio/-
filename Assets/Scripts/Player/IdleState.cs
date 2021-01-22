using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class IdleState : State
{
    private Player      _player;
    private float       _moveX;
    public IdleState(Player player, StateMachine movementSM) 
        :base(movementSM)
    {
        _player = player;
        _movementSM = movementSM;
    }
    public override void Enter()
    {
        base.Enter();
        _player.ani.SetBool("isWalk", false);
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _moveX = _player.Move;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_moveX != 0)
        {
            _movementSM.ChangeState(_player.moveS);
        }
    }
}
