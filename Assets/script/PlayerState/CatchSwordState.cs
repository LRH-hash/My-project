using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ͨ��player��CatchTheSword����,��������ʧ��˲�����
public class CatchSwordState : PlayerState
{
    private Transform sword;

    public CatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.Sword.transform;//ͨ��player���sword�õ�����λ�ã����ߴ�������һ��Transform sword��������player���sword
        if (player.transform.position.x > sword.position.x && player.moveRight == 1)//��aim��һ���ĵ���Flip����
        {
            player.FlipController();
        }
        else if (player.transform.position.x < sword.position.x && player.moveRight == -1)
        {
            player.FlipController();
        }

     /*   rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);//��rb.velocity�����ٶȼ��ɣ�����SetVelocity��Ϊ��ص��½�ɫ��ת������ɫֻ�ǳ�������ķ������ƶ�*/
    }

    public override void Exit()
    {
        base.Exit();
       /* player.StartCoroutine("BusyFor", .1f);*/ //���ý�ɫ����׼���һ˲����ûؽ���һ˲��ʱ�����ƶ�

    }

    public override void Update()
    {
        base.Update();
        if (player.Trigger)//ͨ��triggerCalled�����˳�����idleState����catch���������˳�
        {
           StateMachine.ChangeState(player.idolState);
            player.Trigger = false;
        }
    }
}