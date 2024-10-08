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
                    if (isoneclone)
                    {
                        SkillManager.instance.clone.CreateClonecounterAttack(i.transform);
                        isoneclone = false;
                    }
                   player.charactState.Dodamage(i.GetComponent<CharactState>());
                }
            }
        }
        if (statetimer < 0 || player.isattack ==false)
        {
            StateMachine.ChangeState(player.idolState);
        }
    }
}
