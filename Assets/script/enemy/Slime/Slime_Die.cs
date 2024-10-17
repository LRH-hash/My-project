using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Die : enemyState
{
    public Enemy_Slime slime;
    public Slime_Die(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname)
    {
        slime = _Slime;
    }

    public override void Enter()
    {

        base.Enter();
        statetimer = .1f;
        slime.FreezeTimer(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(statetimer<0)
        {
            slime.rb.velocity = Vector2.zero;
        }
    }
}
