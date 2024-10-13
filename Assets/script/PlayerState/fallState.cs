using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallState : PlayerState
{
    public fallState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
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
        if (rb.velocity.y == 0&&player.isGround())
            StateMachine.ChangeState(player.idolState);
        rb.velocity = new Vector2(xinput * player.movespeed, rb.velocity.y);
        if(player.iswallGround()&&xinput!=0)
        {
            StateMachine.ChangeState(player.wallstate);
        }
        player.fX.CreateAfterImage();
    }

    // Start is called before the first frame update

}
