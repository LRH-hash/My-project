using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anotherenemy : MonoBehaviour
{
    public float enemyspeed = 2;
    public Transform target;
    public Animator anim;
    public int hp = 100;
    public bool movingRight = true;
    public float max = 10f;
    public float min = -10f;
    public float attackrotate = 2;
    public float attacktime;
    public int attack = 20;
    void Start()
    {
        anim = GetComponent<Animator>();
        max = transform.position.x + 10f;
        min = transform.position.x - 10f;

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Move();
            anim.SetBool("ismove", true);
        }
        else
        {
            if (Vector2.Distance(target.position, transform.position) > 2.1f)
            {
                if (target.position.x > transform.position.x)
                {
                    transform.eulerAngles = new Vector2(0, 0);
                    movingRight = true;
                }
                else if (target.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector2(0, 180);
                    movingRight = false;
                }

                Vector3 offset = target.position - transform.position;
                offset.y = 0;
                transform.position += offset.normalized * Time.deltaTime * enemyspeed;
            }
            else
            {

                if (Time.time > attacktime)
                {
                    anim.SetTrigger("isattack");
                    attacktime = Time.time + attackrotate;
                }
            }
        }
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }
    public void leaveTarget()
    {
        target = null;
    }
    void Move()
    {
        if (movingRight)
        {
            // 向右移动  
            transform.position += new Vector3(enemyspeed * Time.deltaTime, 0, 0);

            // 检查是否到达边界  
            if (transform.position.x >= max)
            {
                movingRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
                // 到达边界，改变方向  
            }
        }
        else
        {
            // 向左移动  
            transform.position += new Vector3(-enemyspeed * Time.deltaTime, 0, 0);

            // 检查是否到达边界  
            if (transform.position.x <= min)
            {
                movingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                // 到达边界，改变方向  
            }
        }
    }
    public void attacked(int damage)
    {;
        hp -= damage;
        anim.SetTrigger("ishurt");
        if (hp <= 0)
        {
            anim.SetTrigger("isdead");
            Destroy(this.gameObject, 2);
        }
    }
}
/*public void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.tag == "Player")
    {
        collision.GetComponent<Player>().hurt(attack);
    }
}

*/