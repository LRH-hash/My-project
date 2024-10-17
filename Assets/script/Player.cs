using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : entity
{
    public float movespeed = 8;
    public float dashspeed = 12;
    public Vector2[] attackmovement;
    public PlayerStateMachine StateMachine { get; private set; }    
    public float jumpspeed=6;
    public float counterattacktimer;
    public float dir;
    public float dashduringtime=0.3f;
    public float dashcooltimer;
    public float dashwaittime = 1f;
    public bool successfulcounterattack = false;
    public GameObject Sword;
    public bool Trigger = false;
    public float blackHoleDuringtime = 0.3f;
  
    #region State
    public idolState idolState { get; private set; }
    public moveState movestatae { get; private set; }
    public fallState fallstate { get; private set; }
    public jumpState jumpstate { get; private set; }
   public DashState dashState { get; private set; }
    public wallState wallstate { get; private set; }
    public walljumpState walljumpstate { get; private set; }
    public PrimaryattackState primaryattackstate { get; private set; }
    public CounterAttackState counterAttackState { get; private set; }
    public AimSwordState aimSwordState { get; private set; }
    public CatchSwordState catchSwordState { get; private set; }
    public blackholeState BlackholeState { get; private set; }
    public DieState diestate { get; private set; }
    public float DefaultSpeed;
    public float DefaultjumpSpeed;
    public float DefaultDashSpeed;
    #endregion
    private  void Awake()
    {
        StateMachine = new PlayerStateMachine();
        idolState = new idolState(this, StateMachine, "idle");
        movestatae = new moveState(this, StateMachine, "move");
        jumpstate = new jumpState(this, StateMachine, "jump");
        fallstate = new fallState(this, StateMachine, "jump");
        dashState = new DashState(this, StateMachine, "dash");
        wallstate = new wallState(this, StateMachine, "wallslide");
        walljumpstate = new walljumpState(this, StateMachine,"walljump");
        primaryattackstate = new PrimaryattackState(this, StateMachine, "attack");
        counterAttackState = new CounterAttackState(this, StateMachine, "counterattack");
        aimSwordState = new AimSwordState(this, StateMachine, "AimSword");
        catchSwordState = new CatchSwordState(this, StateMachine, "CatchSword");
        BlackholeState = new blackholeState(this, StateMachine, "jump");
        diestate = new DieState(this, StateMachine, "Die");
        StateMachine.Init(idolState);
        DefaultSpeed = movespeed;
        DefaultjumpSpeed = jumpspeed;
        DefaultDashSpeed = dashspeed;
    }  
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (Time.timeScale == 0)
            return;
        base.Update();
        dashcooltimer -= Time.deltaTime;
        StateMachine.currentState.Update();
        anim.SetFloat("velocity.y", rb.velocity.y);
        if ( Input.GetKeyDown(KeyCode.LeftShift)&&SkillManager.instance.dash.CanUseSkill())
        {
            StateMachine.ChangeState(dashState);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            SkillManager.instance.cyrstal.CanUseSkill();
        }
        if(StateMachine.currentState==diestate)
        {
          /*  Destroy(gameObject, 2);保留，先不销毁*/
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.UseFlask();
        }
    }
    public void SetTrigger()
    {
        Trigger = true;
    }
    public void ExitBlackHole()
    {
        StateMachine.ChangeState(fallstate);
    }
    public void PlayerAttackCheck()
    {
       /* AudioManager.instance.PlaySFX(2);*/
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkattack.position, checkattackRidius);
        foreach(var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {
                anim.speed =0;
                Invoke("ReturnAnimSpeed", stopanimtime);
               audiosource.clip = AudioManager.instance.sfx[2];
                audiosource.Play();
                charactState.Dodamage(i.GetComponent<EnemyStats>(),transform);
               ItemData_equirment weapon=Inventory.Instance.GetEquipment(equirmentType.Weapon);
                weapon?.ExecuteitemEffect(i.transform);
            }
        }
    }
    public void GetSword(GameObject _newSword)
    {
        Sword = _newSword;
    }
    public void CatchSword()
    {
        StateMachine.ChangeState(catchSwordState);
        Destroy(Sword);
    }
    public void CreateSword()
    {
        SkillManager.instance.Sword.CreateSword();
    }

    public override void SlowEntry(float _SlowPercent, float DurinyTIme)
    {
        base.SlowEntry(_SlowPercent, DurinyTIme);
        movespeed=movespeed * (1 - _SlowPercent);
        jumpspeed=jumpspeed * (1 - _SlowPercent);
        dashspeed=dashspeed * (1 - _SlowPercent);
        anim.speed = (1 - _SlowPercent);
        Invoke("ReturnDefault", DurinyTIme);
    }
    public override void ReturnDefault()
    {
        base.ReturnDefault();
        movespeed = DefaultSpeed;
        jumpspeed = DefaultjumpSpeed;
        dashspeed = DefaultDashSpeed;
    }
    public override void SetupZeroKnockPower()
    {
        KnockedBackDirection = Vector2.zero;
    }
    public void PrefectDefend()
    {
        if (charactState.PrefectDefend)
            charactState.PrefectDefend = false;
        else
            charactState.PrefectDefend = true;
    }
    public void Defend()
    {
            charactState.Defend = true;
    }
}
