using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : PlayerState
{
    // Start is called before the first frame update
    public GroundState(Player _player, PlayerStateMachine _playerStateMachine, string _animname) : base(_player, _playerStateMachine, _animname)
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            StateMachine.ChangeState(player.BlackholeState);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StateMachine.ChangeState(player.primaryattackstate);
         /*   rb.drag = 100f;*/
                //设置摩檫力可能会导致转向，摩檫力可能给予负的velocity;
        }
        if (!player.isGround())
        {
            StateMachine.ChangeState(player.fallstate);
        }
        if (player.isGround()&&Input.GetKeyDown(KeyCode.Space))
        {
           
            StateMachine.ChangeState(player.jumpstate);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StateMachine.ChangeState(player.counterAttackState);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)&&HasnoSword())
        {
            StateMachine.ChangeState(player.aimSwordState);
        }
    }
    public bool HasnoSword()
    {
        if (player.Sword == null)
            return true;
        else
            player.Sword.GetComponent<Sword_SkillController>().MoveToPlayer();
            return false;

    }
}
