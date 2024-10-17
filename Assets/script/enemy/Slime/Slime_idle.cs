using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_idle : Slime_GroundState
{
    public Slime_idle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname,_Slime)
    {
        
    }

    public override void Enter()
    {
        statetimer = 1f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (statetimer < 0 && slime.anim.speed != 0)
        {
            enemyStateMachine.ChangeState(slime.slime_move);
        }
    }
}
