using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Controller : MonoBehaviour
{
    public CharactState _Stat;
    public GameObject boomprefab;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ground")
        {
            GameObject boom=Instantiate(boomprefab, transform.position, Quaternion.identity);        
            boom.GetComponent<Boom_Controller>()._enemystat = _Stat;
            Destroy(gameObject);
        }
    }
}
