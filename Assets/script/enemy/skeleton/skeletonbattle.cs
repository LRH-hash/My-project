using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonbattle : enemyState
{
    public skeleton skeleton;
    public Transform Player;
    float dir = 1;
    // Start is called before the first frame update
    public skeletonbattle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        Player = PlayerManager.instance.player.transform;
        statetimer = skeleton.battletime;
        if (Player.GetComponent<PlayerStats>().isDie)
            enemyStateMachine.ChangeState(skeleton.Skeletonmove);
    }

    public override void Exit()
    {
        base.Exit();
        statetimer = 0;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (Player.position.x > skeleton.transform.position.x)
            dir = 1;
        else if (Player.position.x < skeleton.transform.position.x)
            dir = -1; 
        skeleton.SetVelocity(skeleton.pursuitspeed * dir, rb.velocity.y);
        if(Vector2.Distance(Player.position,skeleton.transform.position)<skeleton.attackDistance)
        {
            if (skeleton.canattack())
                enemyStateMachine.ChangeState(skeleton.Skeletonattack);
            else
            {
                skeleton.SetVelocity(0, 0);
            }
        }
        if(Vector2.Distance(Player.position, skeleton.transform.position) >10f||statetimer<0)
        {
            enemyStateMachine.ChangeState(skeleton.skeletonIdle);
        }
    }
}
