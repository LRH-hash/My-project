using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_spellcast : enemyState
{
    public Boss enemy;
    public float amountspell;
    public Boss_spellcast(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss _Boss) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Boss;
    }

    public override void Enter()
    {
        amountspell = enemy.spellAmount;
        statetimer = 0.2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastspellcasttime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (canspellcast())
            enemy.createFireCircle();
        if (amountspell <= 0)
            enemyStateMachine.ChangeState(enemy.boss_Battle);
    }
    public bool canspellcast()
    {
        if (amountspell > 0&& statetimer < 0 )
        {
            statetimer = 1f;
            amountspell--;
            for(int i=0;i<7;i++)
            {
                enemy.createFireCircle();
            }
            return true;
        }
        else
            return false;
    }
}
