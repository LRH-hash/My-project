using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Stun : enemyState
{
    public Enemy_Slime slime;
    public Slime_Stun(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Enemy_Slime _Slime) : base(_enemy, _enemyStateMachine, _animname)
    {
        slime = _Slime;
    }

    public override void Enter()
    {
        base.Enter();
        statetimer = slime.stunDuring;
        slime.isknocked = true;
        rb.velocity = new Vector2(slime.stunDirection.x * slime.player.moveRight, slime.stunDirection.y);
        slime.fX.InvokeRepeating("RedBlink", 0, .1f);
    }

    public override void Exit()
    {
        base.Exit();
        slime.charactState.Invincible = false;
        rb.velocity = new Vector2(0, 0);
        slime.isknocked = false;
  
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < .1f && slime.isGround())
        {
            slime.charactState.Invincible = true;
            slime.fX.Invoke("whiltBlink", 0);
        }
        if (statetimer < 0 && slime.anim.speed != 0)
        {
            enemyStateMachine.ChangeState(slime.slime_Idle);
        }
    }
}
