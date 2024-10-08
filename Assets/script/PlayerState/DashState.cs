using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        statetimer = player.dashduringtime;
        skill.clone.CreateCloneOnDashStart() ;

    }

    public override void Exit()
    {
      
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        player.dashcooltimer = player.dashwaittime;
        skill.clone.CreateCloneOnDashEnd();

    }


    public override void Update()
    {
        base.Update();
        if (xinput != 0)
        {
            player.SetVelocity(player.dashspeed * xinput, 0);
        }
        else
        { 
            player.SetVelocity(player.dashspeed * player.moveRight, 0); 
        }
        if (!player.isGround() && player.iswallGround())
            StateMachine.ChangeState(player.wallstate);
        if (statetimer<0)
        {
            StateMachine.ChangeState(player.idolState);
        
        }
      
    }
}
