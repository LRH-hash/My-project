using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    // Start is called before the first frame update
    public float chanceTolooseTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.Instance;
        List<InventoryItem> current = Inventory.Instance.GetEquirments;
        for(int i=current.Count-1;i>=0; i--)
        {
            if(Random.Range(0,100)<chanceTolooseTime)
            {
                DropItem(current[i].data);
                inventory.UnequirItem(current[i].data as ItemData_equirment);
                //²ÄÁÏ²»µôÂä
            }
        }
        inventory.UpdateUi();
    }
}
