using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpState : PlayerState
{
    // Start is called before the first frame update
    public jumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpspeed);
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
      
        if (rb.velocity.y < 0)
            StateMachine.ChangeState(player.fallstate);
        if (player.iswallGround() && xinput != 0)
        {
            StateMachine.ChangeState(player.wallstate);
        }
        rb.velocity = new Vector2(xinput * player.movespeed, rb.velocity.y);
    }
}
