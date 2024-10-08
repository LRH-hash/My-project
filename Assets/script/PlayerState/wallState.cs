using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallState : PlayerState
{
    // Start is called before the first frame update
    public wallState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
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
        if (xinput * player.moveRight < 0 || xinput == 0)
            StateMachine.ChangeState(player.idolState);
        if(player.isGround()||!player.iswallGround())
            StateMachine.ChangeState(player.idolState);
       
        if (yinput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(player.walljumpstate);
        }
    }
}
