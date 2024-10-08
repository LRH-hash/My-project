using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonattack : enemyState
{
    public skeleton skeleton;
    // Start is called before the first frame update
    public skeletonattack(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.isattack = true;
        skeleton.isknocked = true;
        
    }
 

    public override void Exit()
    {
        base.Exit();
        skeleton.lastattacktime = Time.time;
        skeleton.isknocked = false;
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(0, 0);
        if (!skeleton.isattack)
            enemyStateMachine.ChangeState(skeleton.skeletonBattle);
    }
}
