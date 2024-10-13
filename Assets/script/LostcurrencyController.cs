using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostcurrencyController : MonoBehaviour
{
    public int currency;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()!=null)
        {
            PlayerManager.instance.currentSouls += currency;
            Destroy(this.gameObject);
        }
    }
}
