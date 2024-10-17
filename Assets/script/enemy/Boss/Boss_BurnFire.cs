using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BurnFire : enemyState
{
        public Boss enemy;
        public Boss_BurnFire(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss enemy1) : base(_enemy, _enemyStateMachine, _animname)
        {
            enemy = enemy1;
        }
    // Start is called before the first frame update
    public override void Enter()
    {

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isburn = false;
        enemy.lastburntime = Time.time;
    }
    public override void Update()
    {
        base.Update();
        if (enemy.isburn)
            enemyStateMachine.ChangeState(enemy.boss_Battle);
    }
}

