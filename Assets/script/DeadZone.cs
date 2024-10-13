using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharactState>() != null)
            collision.GetComponent<CharactState>().Die();
        else
            Destroy(collision.gameObject);
    }
}
