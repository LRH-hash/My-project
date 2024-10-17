using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_Controller : MonoBehaviour
{
    public CharactState _enemystat;
    public AudioSource audiosource;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStats>()!=null)
        {
            audiosource.Play();
            _enemystat.Dodamage(collision.GetComponent<PlayerStats>(), transform);
        }
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
