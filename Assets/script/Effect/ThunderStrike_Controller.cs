using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    public GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<enemy>()!=null)
        {
            player.GetComponent<PlayerStats>().MagicDamage(collision.GetComponent<EnemyStats>(),transform);
        }
    }
}
