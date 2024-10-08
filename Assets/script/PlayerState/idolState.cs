using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idolState : GroundState
{
    public idolState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
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
        if (xinput != 0)
        {
            StateMachine.ChangeState(player.movestatae);
        }
    }

    // Start is called before the first frame update
  
}
