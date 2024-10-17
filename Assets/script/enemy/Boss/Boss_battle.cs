using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_battle : enemyState
{
    public Boss enemy;
    public Transform Player;
    float dir = 1;
    // Start is called before the first frame update
    public Boss_battle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss _Boss) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Boss;
    }
    public override void Enter()
    {
        base.Enter();
        Player = PlayerManager.instance.player.transform;
        if (Player.GetComponent<PlayerStats>().isDie)
            enemyStateMachine.ChangeState(enemy.boss_dead);
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
        if (enemy.canSpellCast())
            enemyStateMachine.ChangeState(enemy.boss_Spellcast);
        if (enemy.canburnFire())
            enemyStateMachine.ChangeState(enemy.boss_burnFire);
        if (Vector2.Distance(Player.position, enemy.transform.position) < enemy.attackDistance)
        {
            
           if(enemy.canattack())
                enemyStateMachine.ChangeState(enemy.boss_attack);
            else
            {
                enemyStateMachine.ChangeState(enemy.idle);
            }
        }
        if (Vector2.Distance(Player.position, enemy.transform.position) > 50f)
        {
            enemyStateMachine.ChangeState(enemy.idle);
        }
    }
}
