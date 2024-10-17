using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : enemyState
{
    public Boss enemy;
    public Boss_Attack(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss enemy1) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = enemy1;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isattack = true;
        enemy.isknocked = true;
        enemy.chanceToBurn += 5;
        enemy.audiosource.clip = AudioManager.instance.sfx[6];
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
            enemyStateMachine.ChangeState(enemy.boss_Battle);
        }
    }
}
