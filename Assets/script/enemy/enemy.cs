using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : entity
{
    public float moveSpeed = 3;
    public LayerMask checkplayer;
    public float checkPlayerDistance;
    public float pursuitspeed = 4;
    public float attackcooldown = 0.4f;
    public float lastattacktime;
    public float attackDistance = 2f;
    public float battletime = 6f;
    public Transform playertransform;
    public Player player;
    public Vector2 stunDirection;
    public float stunDuring;
    public bool canStun;
    public float DefaultSpeed;
    public GameObject CountImage;
    public bool isDie = false;

    public enemyStateMachine EnemyStateMachine { get; private set; }
    protected virtual void Awake()
    {

        EnemyStateMachine = new enemyStateMachine();
        playertransform = GameObject.Find("Player").transform;
        player = playertransform.GetComponent<Player>();
        DefaultSpeed = moveSpeed;
  /*      player = PlayerManager.instance.player;
        playertransform = player.transform;*/
    }
    
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        EnemyStateMachine.currentState.Update();
    }
    public bool CheckPlayer() => Physics2D.Raycast(transform.position, Vector2.right*moveRight, checkPlayerDistance, checkplayer);
    public virtual IEnumerator FreezeTimeEnemy(float _Second)
    {
        FreezeTimer(true);
        yield return new WaitForSeconds(_Second);
        FreezeTimer(false);
    }
    public virtual void SetFreezeTime(float _duringtime) => StartCoroutine("FreezeTimeEnemy", _duringtime);
    public void FreezeTimer(bool _Freeze)
    {
        if(_Freeze)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = DefaultSpeed;
            anim.speed = 1;
        }

    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(checkPlayerDistance*moveRight + transform.position.x, transform.position.y));
    }
    public bool canattack()
    {
        if(Time.time>lastattacktime+attackcooldown)
        {
            return true;
        }
        return false;
    }
    public override void Die()
    {

    }
    public virtual void  OpenCountImageWindow()
    {
        canStun = true;
        CountImage.SetActive(true);
    }
    public virtual void CloseCountImageWindow()
    {
        canStun = false;
        CountImage.SetActive(false);
    }
    public virtual bool CanStunWindow()
    {
        if (canStun)
        {
            CloseCountImageWindow();
            return true;
        }
        else
            return false;
    }
    public void EnemyAttackCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkattack.position, checkattackRidius);
        foreach (var i in colliders)
        {
            if (i.GetComponent<Player>() != null)
            {
                charactState.Dodamage(i.GetComponent<PlayerStats>(),transform);           
            }
        }
    }
    public override void SlowEntry(float _SlowPercent, float DurinyTIme)
    {
        base.SlowEntry(_SlowPercent, DurinyTIme);
        moveSpeed = moveSpeed * (1 - _SlowPercent);
        anim.speed=(1 - _SlowPercent);
        Invoke("ReturnDefault", DurinyTIme);
    }

    public override void ReturnDefault()
    {
        base.ReturnDefault();
        moveSpeed = DefaultSpeed;

    }
}
