using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackholeState : PlayerState
{
    public float defaultGravity;
    public bool isUsed=true;
    // Start is called before the first frame update
    public blackholeState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = rb.gravityScale;
        statetimer = player.blackHoleDuringtime;
        rb.gravityScale = 0;
        isUsed = true;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = defaultGravity;
        player.Settransparent(false);
    }

    public override void Update()
    {
        base.Update();
        if(statetimer>0)
        {
            rb.velocity = new Vector2(0, 5);
        }
        if(statetimer<0)
        {
            if (isUsed)
            {     
                rb.velocity=new Vector2(0,0);
                 SkillManager.instance.blackHole.CanUseSkill();
                isUsed = false;
            }
        }
        //退出条件在Black_HoleController里；
        // ExitSkill();
    }
}
