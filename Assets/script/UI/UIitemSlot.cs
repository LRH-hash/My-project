using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

 public class UIitemSlot : MonoBehaviour , IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image Itemimage;
    public TextMeshProUGUI itemtext;
    public InventoryItem items;
    public UI uI;
    private void Start()
    {
        uI = GetComponentInParent<UI>();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (items == null)
            return;
        if(Input.GetKey(KeyCode.Tab))
        {
            Inventory.Instance.RemoveItem(items.data);
                return;
        }
        if (items.data.itemtype == itemType.Equipment)
            Inventory.Instance.EquipMent(items.data);
    }
   
    // Start is called before the first frame update
    public void UpdateItemSlot(InventoryItem _items)
    {
        items = _items;
        Itemimage.color = Color.white;
        if (items != null)
        {
            Itemimage.sprite = items.data.icon;
            if (items.StackSizes > 1)
            {
                itemtext.text = items.StackSizes.ToString();
            }
            else
            {
                itemtext.text = " ";
            }
        }
    }
    public void CleanUpSlot()
    {
        items = null;
        itemtext.text = " ";
        Itemimage.color = Color.clear;
        Itemimage.sprite = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (items == null)
            return;
        uI.itemtip.ShowTip(items.data as ItemData_equirment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(items == null)
            return;
        uI.itemtip.HideTip();
    }
}
