using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_equipmentSlot : UIitemSlot
{
    public equirmentType equirmenttype;
    // Start is called before the first frame update
    public void OnValidate()
    {
        gameObject.name = "EquirmentType-" + equirmenttype.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (items == null||items.data==null)
            return;
        Inventory.Instance.UnequirItem(items.data as ItemData_equirment);
        Inventory.Instance.Additem(items.data as ItemData_equirment);
        uI.itemtip.HideTip();
        CleanUpSlot();

    }
}
