using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_move : Slime_GroundState
{
    public Slime_move(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname, _Slime)
    {

    }

    public override void Enter()
    {
        statetimer = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        slime.SetVelocity(slime.moveSpeed * slime.moveRight, slime.rb.velocity.y);
        if (!slime.isGround() || slime.iswallGround())
        {
            slime.SetVelocity(0, 0);
            slime.FlipController();
            enemyStateMachine.ChangeState(slime.slime_Idle); ;
        }
        if (statetimer < 0 && slime.anim.speed != 0)
        {
            enemyStateMachine.ChangeState(slime.slime_Idle);
        }

    }
}

