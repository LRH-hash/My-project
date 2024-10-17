using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack : enemyState
{
    public Enemy_Slime enemy;
    public Slime_Attack(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Slime;
    }

    public override void Enter()
    {
        enemy.isknocked = true;
        base.Enter();
        enemy.isattack = true;
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
            enemyStateMachine.ChangeState(enemy.Slime_battle);
    }

}
