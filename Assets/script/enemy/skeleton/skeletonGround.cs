using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonGround : enemyState
{
    public skeleton skeleton;
    public skeletonGround(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(skeleton.CheckPlayer()||Vector2.Distance(skeleton.transform.position,skeleton.playertransform.transform.position)<2f)
            enemyStateMachine.ChangeState(skeleton.skeletonBattle);
    }
    // Start is called before the first frame update

}
