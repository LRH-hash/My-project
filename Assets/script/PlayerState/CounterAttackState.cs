using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackState : PlayerState
{
    public bool isoneclone;
    public CounterAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
    {
    }

    // Start is called before the first frame update


    public override void Enter()
    {
        base.Enter();
        isoneclone = true;
        player.isattack = true;
        statetimer = player.counterattacktimer;      
        player.anim.SetBool("successfulcounterattack", false);
        rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.checkattack.position, player.checkattackRidius); ;
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {      
               if( i.GetComponent<enemy>().CanStunWindow())
                {
                    statetimer = 10f;
                    player.anim.SetBool("successfulcounterattack", true);
                    SkillManager.instance.parry.CanUseSkill();
                    if(isoneclone)
                    {
                        isoneclone = false;
                        SkillManager.instance.parry.MakeMirageOnParry(i.transform);
                    }   
                   player.charactState.Dodamage(i.GetComponent<CharactState>(),player.transform);
                }
            }
        }
        if (statetimer < 0 || player.isattack ==false)
        {
            StateMachine.ChangeState(player.idolState);
        }
    }
}
