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
        player.anim.SetBool("successfulcounterattack", false);
        rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
        player.charactState.Defend = false;
        player.charactState.PrefectDefend = false;
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
                    player.stoptime();
                    player.anim.SetBool("successfulcounterattack", true);
                    player.audiosource.clip = AudioManager.instance.sfx[2];
                    player.audiosource.Play();
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
        if (Input.GetKeyUp(KeyCode.Q))
        {
            StateMachine.ChangeState(player.idolState);
        }
    }
    public void successfultanfan(enemy _enemy)
    {
        _enemy.ReturnAnimSpeed();
    }
}
