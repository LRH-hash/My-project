using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_spellcast : enemyState
{
    public enemy_DeathBringer enemy;
    public float amountspell;
    public float cooltimer;
    public Death_spellcast(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, enemy_DeathBringer _Deathenemy) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Deathenemy;
    }

    public override void Enter()
    {
        cooltimer = enemy.spellcooldown;
        amountspell = enemy.spellAmount;
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
        cooltimer -= Time.deltaTime;
        if (canspellcast())
            enemy.Canspell();
        if (amountspell <= 0)
            enemyStateMachine.ChangeState(enemy.death_Battle);
        
    }
    public bool canspellcast()
    {
        if (amountspell > 0 && cooltimer < 0)
        {
            amountspell--;
            cooltimer = enemy.spellcooldown;
            return true;
        }
        else
            return false;
    }

}
