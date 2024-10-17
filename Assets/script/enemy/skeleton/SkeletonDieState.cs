using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class SkeletonDieState : enemyState
{
    public skeleton enemy;
    // Start is called before the first frame update
    public SkeletonDieState(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _skeleton;
    }

    public override void Enter()
    {
        base.Enter(); 
        statetimer = .1f;
        enemy.FreezeTimer(false);
    }

    public override void Update()
    {
        base.Update();
        if(statetimer<0)
        {
            enemy.rb.velocity = Vector2.zero;
            
        }
       
    }

  
}
