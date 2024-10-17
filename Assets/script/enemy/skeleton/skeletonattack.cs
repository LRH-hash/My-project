using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonattack : enemyState
{
    public skeleton enemy;
    // Start is called before the first frame update
    public skeletonattack(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isattack = true;
        enemy.isknocked = true;
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
            enemyStateMachine.ChangeState(enemy.skeletonBattle);
    }
}
