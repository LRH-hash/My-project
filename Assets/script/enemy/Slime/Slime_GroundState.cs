using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_GroundState : enemyState
{
    public Enemy_Slime slime;
    public Slime_GroundState(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname)
    {
        slime = _Slime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(slime.CheckPlayer() || Vector2.Distance(slime.transform.position, slime.playertransform.transform.position) < 2.5f)
        {
            enemyStateMachine.ChangeState(slime.Slime_battle);
        }
    }
}
