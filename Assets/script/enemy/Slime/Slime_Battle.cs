using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Battle : enemyState
{
    public Enemy_Slime slime;
    public float attackTimer;
  
    float dir = 1;
    public Slime_Battle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname)
    {
        slime = _Slime;
    }

    public override void Enter()
    {
        statetimer = slime.battletime;
        if (slime.player.GetComponent<PlayerStats>().isDie)
            enemyStateMachine.ChangeState(slime.slime_move);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        statetimer = 0;
    }

    public override void Update()
    {
        base.Update();
        if (slime.player.transform.position.x > slime.transform.position.x)
            dir = 1;
        else if (slime.player.transform.position.x < slime.transform.position.x)
            dir = -1;
        slime.SetVelocity(slime.pursuitspeed * dir, rb.velocity.y);

        if (Vector2.Distance(slime.player.transform.position, slime.transform.position) < slime.attackDistance)
        {
            if (slime.canattack())
                enemyStateMachine.ChangeState(slime.slime_Attack);
            else
            {
                slime.SetVelocity(0, 0);
            }
        }
        if (Vector2.Distance(slime.player.transform.position, slime.transform.position) > 10f || statetimer < 0)
        {
            enemyStateMachine.ChangeState(slime.slime_Idle);
        }
    }
}
