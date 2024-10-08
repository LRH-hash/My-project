using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveState : GroundState
{
    public moveState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
    }
    public override void Update()
    {
        base.Update();
        player.SetVelocity(xinput*player.movespeed, rb.velocity.y);
        if (xinput==0)
        {
            StateMachine.ChangeState(player.idolState);
        }

    }
    public override void Exit()
    {
        base.Exit();
    }



  

    // Start is called before the first frame update
   
}
