using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryItem
{ 
    public itemData data;
    public int StackSizes;
    // Start is called before the first frame update
    public InventoryItem(itemData _newItemData)
    {
        data = _newItemData;
        StackSizes = 1;
    }
    public void AddStack() => StackSizes++;
    public void RemoveStack() => StackSizes--;
       
}
