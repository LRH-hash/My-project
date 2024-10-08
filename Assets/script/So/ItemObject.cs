using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public itemData item;
    public Rigidbody2D rb;

    private void SetName()
    {
        if (item == null)
            return;
        GetComponent<SpriteRenderer>().sprite = item.icon;
        gameObject.name = "itemObject" + item.itemname;
    }

    // Update is called once per frame
public void Setitem(itemData _item,Vector2 vector2)
    {
        item = _item;
        rb.velocity = vector2;
        SetName();
    }
    public void Pickable()
    {
        if(Inventory.Instance.InventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
            Inventory.Instance.UpdateUi();
            Destroy(gameObject);
        }
        if (!Inventory.Instance.canAdditem()&&item.itemtype==itemType.Equipment)
        {
            rb.velocity = new Vector2(0,7);
            return;
        }
        Inventory.Instance.Additem(item);      
        Destroy(gameObject);
    }
}
