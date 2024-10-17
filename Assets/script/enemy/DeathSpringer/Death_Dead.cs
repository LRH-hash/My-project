using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Dead : enemyState
{
    public enemy_DeathBringer enemy;
    public Death_Dead(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, enemy_DeathBringer _Deathenemy) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Deathenemy;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        statetimer = .1f;
        enemy.FreezeTimer(false);
        AudioManager.instance.Bgm.Stop();
   /*     AudioManager.instance.Bgm.clip = AudioManager.instance.bgm[0];
        AudioManager.instance.Bgm.Play();
        AudioManager.instance.Bgm.loop = false;*/
    }

    public override void Update()
    {
        base.Update();
        if (statetimer < 0)
        {
            enemy.rb.velocity = Vector2.zero;

        }

    }
}
