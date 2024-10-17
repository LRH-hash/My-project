using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : enemy
{
    public bool isburn;
    public Transform head;
    public GameObject FireBallPrefab;
    public bool bossbegan;
    public float burntime;
    public float spellAmount;
    public float spellcooldown;
    public float lastspellcasttime;
    public float lastburntime;
    public float chanceToBurn;
    public BoxCollider2D arena;
    public GameObject firePrefab;
    public Boss_idle idle { get; private set; }
    public Boss_Attack boss_attack { get; private set; }
    public Boss_Dead boss_dead { get; private set; }
    public Boss_battle boss_Battle { get; private set; }
    public Boss_spellcast boss_Spellcast { get; private set; }
    public Boss_BurnFire boss_burnFire { get; private set; }
    public override void Start()
    {
        base.Start();
        idle = new Boss_idle(this, EnemyStateMachine, "Idle",this);
        boss_attack = new Boss_Attack(this, EnemyStateMachine, "Attack", this);
        boss_dead = new Boss_Dead(this, EnemyStateMachine, "Die", this);
        boss_Battle = new Boss_battle(this, EnemyStateMachine, "Move", this);
        boss_Spellcast = new Boss_spellcast(this, EnemyStateMachine, "Cast", this);
        boss_burnFire = new Boss_BurnFire(this, EnemyStateMachine, "Fireball",this);
        EnemyStateMachine.Init(idle);
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        EnemyStateMachine.ChangeState(boss_dead);
        Destroy(gameObject, 1);
    }
    public override void EnemyAttackCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkattack.position, checkattackRidius);
        foreach (var i in colliders)
        {
            if (i.GetComponent<Player>() != null)
            {
                anim.speed = 0;
                Invoke("ReturnAnimSpeed", stopanimtime);
                audiosource.clip = AudioManager.instance.sfx[7];
                audiosource.Play();
                charactState.Dodamage(i.GetComponent<PlayerStats>(), transform);
            }
        }
    }
    public void createFireCircle()
    {
        float x = Random.Range(arena.bounds.min.x + 1, arena.bounds.max.x - 1);
      
        GameObject Fire=Instantiate(firePrefab, new Vector2(x, arena.bounds.max.y-2), Quaternion.Euler(0,0,90));
        Fire.GetComponent<Fire_Controller>()._Stat = charactState;
    }
    public bool canSpellCast()
    {
        if (Time.time > lastspellcasttime + spellcooldown)
        {
            lastspellcasttime = Time.time;
            return true;
        }
        return false;
    }
    public void createFireBall()
    {
        int angle = 45;
       audiosource.clip = AudioManager.instance.sfx[6];
       audiosource.Play();
        for (int i=0;i<7;i++)
        {
            GameObject fIre = Instantiate(FireBallPrefab,head.position,Quaternion.Euler(0,0,angle-15*i));
            fIre.GetComponent<Fireball_controller>().SetFireBall(moveRight, charactState);
        }
        isburn = true;
    }
    public bool canburnFire()
    {
        if (Time.time > lastburntime + burntime)
        {
            return true;
        }
        else
            return false;
    }

}
