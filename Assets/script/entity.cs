using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class entity : MonoBehaviour
{
    [Header("tandao")]
    public float stopanimtime = 0.2f;
    public float stoptimefluid = 0.1f;
    public Vector2 knockOffset;
    public Animator anim;
    public Rigidbody2D rb;
    public LayerMask Ground;
    public float checkGrounddistance = 0.5f;
    public float wallGrounddistance = 0.5f;
    public GameObject checkGround;
    public GameObject wallGround;
    public int moveRight = 1;
    public bool isattack = false;
    public Transform checkattack;
    public float checkattackRidius;
    public hitFX fX;
    public bool isknocked = false;
    public Vector2 KnockedBackDirection;
    public float KnockedTime;
    public SpriteRenderer sr;
    public CharactState charactState;
    public ParticleSystem DustFX;
    public AudioSource audiosource;
    public int knockBackDir { get; private set; } 
    public CapsuleCollider2D cd { get; private set; }
    // Start is called before the first frame update

    public  virtual  void Start()
    {
        audiosource = GetComponent<AudioSource>();
        sr = GetComponentInChildren<SpriteRenderer>();
        fX = GetComponent<hitFX>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        charactState = GetComponent<CharactState>();
        cd = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    public virtual  void Update()
    {
        if(!isknocked)
          Flip();
    }
    public System.Action OnFlip;
    public void ReturnAnimSpeed()
    {
        anim.speed = 1;
    }
    public void startInvoke()
    {
        Invoke("ReturnAnimSpeed", stopanimtime);
    }
    public void stoptime()
    {
        Time.timeScale = 0.2f;
        StartCoroutine("returntime");
    }
    public IEnumerator returntime()
    {
        yield return new WaitForSeconds(stoptimefluid);
        Time.timeScale = 1;
    }

    public void Settransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }
    public void SetVelocity(float _xvelocity, float y_velocity)
    {
        if (isknocked == true)
            return;
        rb.velocity = new Vector2(_xvelocity, y_velocity);
    }
    public bool isGround() => Physics2D.Raycast(checkGround.transform.position, Vector3.down, checkGrounddistance, Ground);
    public bool iswallGround() => Physics2D.Raycast(wallGround.transform.position, Vector2.right * moveRight, wallGrounddistance, Ground);
    public virtual void Die()
    {

    }
    public void FlipController()
    {
        moveRight = moveRight * -1;
        transform.Rotate(0, 180, 0);
        OnFlip?.Invoke();
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkGround.transform.position, new Vector3(checkGround.transform.position.x, checkGround.transform.position.y - checkGrounddistance, 0));
        Gizmos.DrawLine(wallGround.transform.position, new Vector3(wallGround.transform.position.x + moveRight*wallGrounddistance, wallGround.transform.position.y,0));
        Gizmos.DrawWireSphere(checkattack.position, checkattackRidius);

    }
    public virtual void Flip()
    {
        if (rb.velocity.x > 0 && moveRight < 0)
        {
            FlipController();
         /*   Debug.Log(rb.velocity);*/
        }
        else if (rb.velocity.x < -0.2 && moveRight > 0)
        {
            FlipController();
         /*   Debug.Log(rb.velocity);*/
        }

    }
    public virtual void SlowEntry(float _SlowPercent,float DurinyTIme)
    {
        
    }
    public virtual void ReturnDefault()
    {
        anim.speed = 1;
    }
    public virtual void SetKnockDirection(Transform _direction)
    {
        if (_direction.position.x > transform.position.x)
            knockBackDir = -1;
        else
            knockBackDir = 1;
    }
    public void SetupKnockPower(Vector2 Power) => KnockedBackDirection = Power;
    public virtual IEnumerator Knock()
    {
        isknocked = true;
        float xoffset = Random.Range(knockOffset.x, knockOffset.y);
        rb.velocity = new Vector2((xoffset+KnockedBackDirection.x)*knockBackDir,KnockedBackDirection.y);
        yield return new WaitForSeconds(KnockedTime);
        isknocked = false;
        SetupZeroKnockPower();
    }
    public virtual void SetupZeroKnockPower()
    {

    }
    public void stopattack()
    {
        isattack = false;
    }
    public void damageEffect()
    {
        fX.StartCoroutine("FX");
        StartCoroutine("Knock");
    }
    public void DustEffect()
    {
        DustFX.Play();
    }
}
