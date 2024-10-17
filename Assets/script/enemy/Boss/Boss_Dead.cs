using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dead : enemyState
{
    public Boss enemy;
    // Start is called before the first frame update
    public Boss_Dead(enemy _enemy, enemyStateMachine _enemyStateMachine, string _animname, Boss _Boss) : base(_enemy, _enemyStateMachine, _animname)
    {
        enemy = _Boss;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.Bgm.Stop();
/*        AudioManager.instance.Bgm.Play();
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
