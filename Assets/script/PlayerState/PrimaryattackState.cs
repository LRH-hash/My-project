using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryattackState : PlayerState
{
    public float waitnexttime = 1f;
    public float attackcombo;
    public float lastendtime;
    // Start is called before the first frame update
    public PrimaryattackState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xinput = 0;
        player.isattack = true;
        player.SetVelocity(0,0);
        if (Time.time > lastendtime + waitnexttime)
        {
            attackcombo = 0;
  ;
        }
        player.anim.SetFloat("attackcombo", attackcombo);
        #region attackdir ���ƹ������򣬵����﹥����ֱ�ӽ�������״̬��û�õ���������ʹ��
        float attackdir = player.moveRight;
        if (xinput != 0)
            attackdir = xinput;
        #endregion     
        rb.velocity = new Vector2(attackdir*player.attackmovement[(int)attackcombo].x, player.attackmovement[(int)attackcombo].y);
        //���ɱ���Ĺ�����A��
    }

    public override void Exit()
    {
        base.Exit();
        attackcombo=attackcombo+1;  
        attackcombo%=3;
        lastendtime = Time.time;
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
 /*       rb.drag = 0;*/
        if (!player.isattack)
        {
            StateMachine.ChangeState(player.idolState);
        }
    }
}
