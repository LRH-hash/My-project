using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_teleport : enemyState
{
    public enemy_DeathBringer enemy;
    public Death_teleport(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, enemy_DeathBringer _Deathenemy) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Deathenemy;
    }

    public override void Enter()
    {
        statetimer = 1f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.FindPosition();
        enemy.Trigger = false;
    }

    public override void Update()
    {
        base.Update();
        if(enemy.Trigger)
        {
            if (enemy.canSpellCast())
                enemyStateMachine.ChangeState(enemy.death_Spellcast);
            else
                enemyStateMachine.ChangeState(enemy.death_Battle);
        }
    }
}
