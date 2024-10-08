using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonmove : skeletonGround
{
    public skeletonmove(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname, _skeleton)
    {
    }

    // Start is called before the first frame update


    public override void Enter()
    {
        base.Enter();
        statetimer = 2f;  
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed*skeleton.moveRight, skeleton.rb.velocity.y);
        if (!skeleton.isGround() || skeleton.iswallGround())
        {
            skeleton.SetVelocity(0, 0);
            skeleton.FlipController();
            enemyStateMachine.ChangeState(skeleton.skeletonIdle);
        }
        if (statetimer < 0&&skeleton.anim.speed!=0)
        {
            enemyStateMachine.ChangeState(skeleton.skeletonIdle);
        }

    }
    // Start is called before the first frame update

}
