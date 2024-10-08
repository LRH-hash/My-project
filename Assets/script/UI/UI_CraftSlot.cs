using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class UI_CraftSlot :UIitemSlot
{


    public void SetUpCraftSlot(ItemData_equirment _data)//设置CraftSlot的公开函数
    {
        if (_data == null)
            return;
        items.data = _data;
        Itemimage.sprite = _data.icon;
        itemtext.text = _data.name;


    }
    private void OnValidate()
    {
        //UpdateSlots(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        uI.CraftWindow.SetUpCraftWIndow(items.data as ItemData_equirment);
    }
}
