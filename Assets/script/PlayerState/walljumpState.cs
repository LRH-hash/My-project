using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walljumpState : PlayerState
{
    // Start is called before the first frame update
    public walljumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(-player.movespeed * player.moveRight, player.jumpspeed);
        //�ж���ʱ����һ��ʱ�䣬ʱ�䵽�������
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
        if (player.isGround())
            StateMachine.ChangeState(player.idolState);
       
    }
}
