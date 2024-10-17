using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : enemy
{

    public skeletonidle skeletonIdle { get; private set; }
    public skeletonmove Skeletonmove { get; private set; }
    public skeletonbattle skeletonBattle { get; private set; }
    public skeletonattack Skeletonattack { get; private set; }
    public SkeletonStun skeletonStun { get; private set; }
    public SkeletonDieState skeletonDieState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        skeletonIdle = new skeletonidle(this, EnemyStateMachine, "idle", this);
        Skeletonmove = new skeletonmove(this, EnemyStateMachine, "move", this);
        skeletonBattle = new skeletonbattle(this, EnemyStateMachine, "move", this);
        Skeletonattack = new skeletonattack(this, EnemyStateMachine, "attack", this);
        skeletonStun = new SkeletonStun(this, EnemyStateMachine, "stun", this);
        skeletonDieState = new SkeletonDieState(this, EnemyStateMachine, "Die", this);
    }
    public override void Start()
    {
        base.Start();
        EnemyStateMachine.Init(skeletonIdle);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {
            EnemyStateMachine.ChangeState(skeletonStun);
        }
        if (anim.speed == 0)
        {
            rb.velocity = Vector2.zero;
        }
        if (isDie)
        {
            EnemyStateMachine.ChangeState(skeletonDieState);
            Destroy(this.gameObject, 2f);
        }

    }
    public override bool CanStunWindow()
    {
        if (base.CanStunWindow())
        {
            EnemyStateMachine.ChangeState(skeletonStun);
            return true;
        }
        else
            return false;
    }

}
