using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_idle : enemyState
{
    public Boss enemy;
    public Boss_idle(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss _Boss) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Boss;
    }

    public override void Enter()
    {
        base.Enter();
        statetimer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!enemy.bossbegan)
            enemy.charactState.health_UI.SetActive(false);
        if (Vector2.Distance(enemy.player.transform.position, enemy.transform.position) < 10f&&!enemy.bossbegan)
        {
            AudioManager.instance.Bgm.clip = AudioManager.instance.bgm[3];
            AudioManager.instance.Bgm.Play();
            AudioManager.instance.Bgm.loop = true;
            enemy.charactState.health_UI.SetActive(true); ;
            enemy.bossbegan = true;
        }
        if (statetimer < 0 && enemy.bossbegan)
        {
            enemyStateMachine.ChangeState(enemy.boss_Battle);
        }
    }
}
