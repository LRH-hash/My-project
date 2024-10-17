using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_attack : enemyState
{
    public enemy_DeathBringer enemy;
    public Death_attack(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, enemy_DeathBringer _Deathenemy) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Deathenemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isattack = true;
        enemy.isknocked = true;
        enemy.chanceToteleport += 5;
        enemy.audiosource.clip = AudioManager.instance.sfx[1];
        enemy.audiosource.Play();

    }


    public override void Exit()
    {
        base.Exit();
        enemy.lastattacktime = Time.time;
        enemy.isknocked = false;
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;
        if (!enemy.isattack)
        {
            if(enemy.canteleport())
            enemyStateMachine.ChangeState(enemy.death_Teleport);
            else
                enemyStateMachine.ChangeState(enemy.death_Battle);
        }
    }
}
