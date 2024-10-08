using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonidle : skeletonGround
{
   
    public skeletonidle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname, _skeleton)
    {
    }

    // Start is called before the first frame update


    public override void Enter()
    {
        base.Enter();
        statetimer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (statetimer < 0&&skeleton.anim.speed != 0)
        {
            enemyStateMachine.ChangeState(skeleton.Skeletonmove);
        }
      
    }
}
