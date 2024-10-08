using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//通过player的CatchTheSword进入,及当剑消失的瞬间进入
public class CatchSwordState : PlayerState
{
    private Transform sword;

    public CatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.Sword.transform;//通过player里的sword拿到剑的位置，作者创建了另一个Transform sword的来保存player里的sword
        if (player.transform.position.x > sword.position.x && player.moveRight == 1)//和aim里一样的调用Flip函数
        {
            player.FlipController();
        }
        else if (player.transform.position.x < sword.position.x && player.moveRight == -1)
        {
            player.FlipController();
        }

     /*   rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);//用rb.velocity设置速度即可（不用SetVelocity因为这回导致角色翻转，而角色只是朝着面向的反方向移动*/
    }

    public override void Exit()
    {
        base.Exit();
       /* player.StartCoroutine("BusyFor", .1f);*/ //设置角色在瞄准后的一瞬间和拿回剑的一瞬间时不能移动

    }

    public override void Update()
    {
        base.Update();
        if (player.Trigger)//通过triggerCalled控制退出进入idleState，及catch动画结束退出
        {
           StateMachine.ChangeState(player.idolState);
            player.Trigger = false;
        }
    }
}