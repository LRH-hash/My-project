using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{

    public int normalattack = 50;
    public float attackcooldown = 2;
    public float attacktime = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
  /*  public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && GetComponentInParent<Player>().isattack== true)
        {
            if (Time.time > attacktime)
            {
                collision.GetComponent<enemy>().attacked(normalattack);
                attacktime = Time.time + attackcooldown;
            }
        }
    }
}*/