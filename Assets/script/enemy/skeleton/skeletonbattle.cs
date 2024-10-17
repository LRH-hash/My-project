using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonbattle : enemyState
{
    public skeleton enemy;
    public Transform Player;
    float dir = 1;
    // Start is called before the first frame update
    public skeletonbattle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname,skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        Player = PlayerManager.instance.player.transform;
        statetimer = enemy.battletime;
        if (Player.GetComponent<PlayerStats>().isDie)
            enemyStateMachine.ChangeState(enemy.Skeletonmove);
    }

    public override void Exit()
    {
        base.Exit();
        statetimer = 0;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (Player.position.x > enemy.transform.position.x)
            dir = 1;
        else if (Player.position.x < enemy.transform.position.x)
            dir = -1; 
        enemy.SetVelocity(enemy.pursuitspeed * dir, rb.velocity.y);
        if(Vector2.Distance(Player.position,enemy.transform.position)<enemy.attackDistance)
        {
            if (enemy.canattack())
                enemyStateMachine.ChangeState(enemy.Skeletonattack);
            else
            {
                enemy.SetVelocity(0, 0);
            }
        }
        if(Vector2.Distance(Player.position, enemy.transform.position) >10f||statetimer<0)
        {
            enemyStateMachine.ChangeState(enemy.skeletonIdle);
        }
    }
}
