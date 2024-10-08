using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSwordState : PlayerState
{
    public AimSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.Sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.transform.position.x > mouseposition.x && player.moveRight == 1)//和aim里一样的调用Flip函数
        {
            player.FlipController();
        }
        else if (player.transform.position.x < mouseposition.x && player.moveRight == -1)
        {
            player.FlipController();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StateMachine.ChangeState(player.idolState); 
        }
        rb.velocity = Vector2.zero;
    }
}
