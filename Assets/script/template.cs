using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class template : MonoBehaviour
{
    #region anim
    public Animator anim;
    public Rigidbody2D rb;
    public float RayDistance=0.5f;
    public LayerMask Ground;
    #endregion
    public bool isGround = true;
    public bool isjump = false;
    public bool ismove = false;

    public float hp = 100;
    public int moverRight = 1;

    //attack
    public Transform wallcheck;
    public float wallcheckdistance = 0.5f;
    public bool isattack = false;
    public float attacktime;
    public float attackcooltime = 0.5f;
    public GameObject checkGrond;
 
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (wallcheck == null)
            wallcheck = transform;
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        Collision();
        flip();
    }
    public virtual void Collision()
    {
       
        isGround = Physics2D.Raycast(checkGrond.transform.position, Vector2.down, RayDistance, Ground);
   
    }
    public void flip()
    {
        if (rb.velocity.x > 0 && moverRight < 0)
        {
            transform.Rotate(0, 180, 0);
            moverRight = 1;
        }

        else if (rb.velocity.x < 0 && moverRight > 0)
        {

            transform.Rotate(0, 180, 0);
            moverRight = -1;
        }
        return;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkGrond.transform.position, new Vector3(checkGrond.transform.position.x,checkGrond.transform.position.y - RayDistance));
        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x + wallcheckdistance, wallcheck.position.y));
    }
}
