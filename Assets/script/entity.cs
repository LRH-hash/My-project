using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
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
    public CapsuleCollider2D cd { get; private set; }
    // Start is called before the first frame update

    public  virtual  void Start()
    {
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
    public virtual IEnumerator Knock()
    {
        isknocked = true;
        rb.velocity = new Vector2(KnockedBackDirection.x*-moveRight,KnockedBackDirection.y);
        yield return new WaitForSeconds(KnockedTime);
        isknocked = false;
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
}
