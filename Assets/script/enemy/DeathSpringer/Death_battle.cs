using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_battle : enemyState
{
    public enemy_DeathBringer enemy;
    public Transform Player;
    float dir = 1;
    // Start is called before the first frame update
    public Death_battle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, enemy_DeathBringer _Deathenemy) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Deathenemy;
    }
    public override void Enter()
    {
        base.Enter();
        Player = PlayerManager.instance.player.transform;
        if (Player.GetComponent<PlayerStats>().isDie)
            enemyStateMachine.ChangeState(enemy.death_move);
    }

    public override void Exit()
    {
        base.Exit();
        statetimer = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Player.position.x > enemy.transform.position.x)
            dir = 1;
        else if (Player.position.x < enemy.transform.position.x)
            dir = -1;
        enemy.SetVelocity(enemy.pursuitspeed * dir, rb.velocity.y);
        if (Vector2.Distance(Player.position, enemy.transform.position) < enemy.attackDistance)
        {
            if (enemy.canattack())
                enemyStateMachine.ChangeState(enemy.death_Attack);
            else
            {
                enemy.SetVelocity(0, 0);
            }
        }
        if (Vector2.Distance(Player.position, enemy.transform.position) > 50f )
        {
            enemyStateMachine.ChangeState(enemy.death_move);
        }
    }
}

