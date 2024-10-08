using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyState 
{
    public float statetimer;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    public enemy Enemy;
    public enemyStateMachine enemyStateMachine;
    public string animname;
   

    public enemyState(enemy _enemy,enemyStateMachine _enemyStateMachine,string _animname)
    {
        this.Enemy = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
        this.animname = _animname;
    }

    // Update is called once per frame
    public virtual void Update()
    {;
        statetimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        rb = Enemy.rb;
        Enemy.anim.SetBool(animname,true);

    }
    public virtual void Exit()
    {
        Enemy.anim.SetBool(animname,false);
    }
}
