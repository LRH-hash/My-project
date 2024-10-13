using System.Collections.Generic;
using UnityEngine;

public class Sword_SkillController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public CircleCollider2D cd;
    public bool canRotate = true;
    public bool returning = false;
    public Player player;
    public float movespeed = 3;
    public List<enemy> enemyList;
    public bool bounce;
    public int enemyCount;
    public float bouncecount=0;
    public float bounceMaxCount;
    public float bounceSpeed = 10;
    public int PierceMaxCount;
    public float FreezeTime;
    [Header("SPIN")]
    public float spinDuringTime;
    public float hitcooldown;
    public float hitDuringTime;
    public bool isSpin;
    public float MaxDistance;
    public bool wasStop;
    public bool TriggerEnemy;
    public float spinDirection;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }
        if (returning)
        {
            transform.right = rb.velocity;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);
            if (Vector2.Distance(PlayerManager.instance.player.transform.position, transform.position) < 1f)
            {
                PlayerManager.instance.player.CatchSword();
            }
        }

        BounceLogic();
        SpinLogic();
        if (Vector2.Distance(player.transform.position, transform.position) > 30f)
            DestroyMe();
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    private void SpinLogic()
    {
        if (isSpin)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > MaxDistance || TriggerEnemy)
            {
                wasStop = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
            if (wasStop)
            {
                spinDuringTime -= Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1.5f * Time.deltaTime);
                if (spinDuringTime < 0)
                {
                    isSpin = false;
                    returning = true;
                }
                hitDuringTime -= Time.deltaTime;
                if (hitDuringTime < 0)
                {
                    hitDuringTime = hitcooldown;
                    Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 1f);

                    foreach (var hit in collisions)
                    {
                       if(hit.GetComponent<enemy>()!=null)
                        EnemyFreeze(hit);
                    }

                }
            }
        }
    }

    public void BounceLogic()
    {
        if (bounce && enemyList.Count>0 )
        {
            if (enemyList[enemyCount] == null)
                return;
          transform.position = Vector2.MoveTowards(transform.position, enemyList[enemyCount].transform.position, bounceSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyList[enemyCount].transform.position) < 0.2f)
            {
                enemyCount++;
                bouncecount++;
                if (enemyCount >= enemyList.Count)
                    enemyCount = 0;
                if (bouncecount >= bounceMaxCount)
                {
                    bounce = false;
                    returning = true;
                }
            }
        }
    }
  
    public void BounceSet(bool isBounce,float _BounceMax)
    {
        bounce = isBounce;
        bounceMaxCount = _BounceMax;
    }
    public void PierceSet(int _PierceCount)
    {
        PierceMaxCount = _PierceCount;
    }
    public void SetupSword(Vector2 direction, float Gravity,Player _player,float _Freeze)
    {
        rb.velocity = direction;
        rb.gravityScale = Gravity;
        player = _player;
        FreezeTime = _Freeze;
        if(PierceMaxCount<=0)
        anim.SetBool("Rotation", true);
        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
    }
    public void SpinSet(bool _isspin,float _maxdistance,float _RotateTime,float hitDuringtime)
    {
        isSpin = _isspin;
        MaxDistance = _maxdistance;
        spinDuringTime = _RotateTime;
        hitcooldown = hitDuringtime;
    }
    public void MoveToPlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        returning = true;
        anim.SetBool("Rotation", true);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (returning)
            return;
        if (isSpin)
        {
            if (collision.GetComponent<enemy>() != null)
                TriggerEnemy = true;
            return;
        }
       if( collision.GetComponent<enemy>()!=null)
        {
            EnemyFreeze(collision);
        }
        if (collision.GetComponent<enemy>() != null && enemyList.Count <= 0)
        {
            if (bounce)
            {
                Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 10f);

                foreach (var hit in collisions)
                {
                    if (hit.GetComponent<enemy>() != null)
                    {
                        enemyList.Add(hit.GetComponent<enemy>());
                    }
                }
            }
        }
        collision_failure(collision);
    }

    private void EnemyFreeze(Collider2D collision)
    {
        enemy _enemy = collision.GetComponent<enemy>();
        if (_enemy != null)
        {
            player.charactState.Dodamage(_enemy.GetComponent<CharactState>(),transform);
            _enemy.StartCoroutine("FreezeTimeEnemy", FreezeTime);
            ItemData_equirment equirment = Inventory.Instance.GetEquipment(equirmentType.Amult);
            if (equirment != null)
                equirment.ExecuteitemEffect(collision.transform);
        }
    }

    public void collision_failure(Collider2D collision)
    {
        if (PierceMaxCount > 0 && collision.GetComponent<enemy>() != null)
        {
            PierceMaxCount--;
            return;
        }

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (bounce&&enemyList.Count>0)       
            return;      
        cd.enabled = false;
        canRotate = false;
        anim.SetBool("Rotation", false);
        GetComponentInChildren<ParticleSystem>().Play();
        transform.parent = collision.transform;
    }
    public void PierceSwitchPosition(Transform _player)
    {
        if(PierceMaxCount>0)
        _player.transform.position = transform.position;
    }
}
