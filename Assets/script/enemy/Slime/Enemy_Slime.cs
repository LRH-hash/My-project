using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slimeType
{
    big,
    middle,
    small
}
public class Enemy_Slime : enemy
{
    [Header("slime prefab")]
    public slimeType slimetype;
    public GameObject slimeprefab;
    public Vector2 slimeminDistance;
    public Vector2 slimeMaxDistance;
    public float slideAmount;
    public Slime_idle slime_Idle { get; private set; }
    public Slime_move slime_move { get; private set; }
    public Slime_Die slime_die { get; private set; }
    public Slime_Attack slime_Attack { get; private set; }
    public Slime_Stun slime_stun { get; private set; }
    public Slime_Battle Slime_battle { get; private set; }

    public override void Start()
    {
        base.Start();
        slime_Idle = new Slime_idle(this, EnemyStateMachine, "Idle", this);
        slime_move=new Slime_move(this, EnemyStateMachine, "Move", this);
        slime_die=new Slime_Die(this, EnemyStateMachine, "Die", this);
        slime_Attack=new Slime_Attack(this, EnemyStateMachine, "Attack", this);
        slime_stun=new Slime_Stun(this, EnemyStateMachine, "Stun", this);
        Slime_battle = new Slime_Battle(this, EnemyStateMachine, "Move", this);
        EnemyStateMachine.Init(slime_Idle);

    }

    public override void Update()
    {
        base.Update();
        if (anim.speed == 0)
        {
            rb.velocity = Vector2.zero;
        }
        if (isDie)
        {
            EnemyStateMachine.ChangeState(slime_die); ;
            Destroy(this.gameObject, 2f);
            if (slimetype == slimeType.small)
                return;
            createSlime();
            isDie = false;
        }
    }
    public void createSlime()
    {
        for(int i=0;i<slideAmount;i++)
        {
            GameObject slime = Instantiate(slimeprefab,transform.position,Quaternion.identity);
            slime.GetComponent<Enemy_Slime>().SetSlime(moveRight);
        }
    }
    public void SetSlime(int _moveright)
    {
        if (moveRight != _moveright)
            FlipController();
        float x = Random.Range(slimeminDistance.x, slimeMaxDistance.x);
        float y = Random.Range(slimeminDistance.y, slimeMaxDistance.y);
        isknocked = true;
        GetComponent<Rigidbody2D>().velocity=new Vector2(x*moveRight, y);
        Invoke("closeKnocke", 1f);
    }
    public void closeKnocke() => isknocked = false;
    public override bool CanStunWindow()
    {
        if (base.CanStunWindow())
        {
            EnemyStateMachine.ChangeState(slime_stun);
            return true;
        }
        else
            return false;
    }

    // Start is called before the first frame update

}
