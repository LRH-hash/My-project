using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    public ItemObject itemobject;
    private void Awake()
    {
        itemobject = GetComponentInParent<ItemObject>();
    }
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {       
            if(collision.GetComponent<CharactState>().isDie==false)
            itemobject.Pickable();
        }
    }
}
