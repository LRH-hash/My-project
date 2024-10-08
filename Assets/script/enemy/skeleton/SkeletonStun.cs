using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStun : enemyState
{
    public skeleton skeleton;
    public SkeletonStun(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        statetimer = skeleton.stunDuring;
        skeleton.isknocked = true;
        rb.velocity = new Vector2(skeleton.stunDirection.x * skeleton.player.moveRight, skeleton.stunDirection.y);
        skeleton.fX.InvokeRepeating("RedBlink", 0, .1f);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = new Vector2(0, 0);
        skeleton.isknocked = false;
        skeleton.fX.Invoke("whiltBlink",0);
    }

    public override void Update()
    {
        base.Update();
        if(statetimer<0&&skeleton.anim.speed != 0)
        {
            enemyStateMachine.ChangeState(skeleton.skeletonIdle);
        }
    }
}
