using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour,IPointerDownHandler
{
    public Transform CraftSlotParent;
    public GameObject craftSlotPrefab;
    public List<UI_CraftSlot> craftSlots;
    public List<ItemData_equirment> craftEquirment;
    

     
    // Start is called before the first frame update
    void Start()
    {
        AssignCraftSlots();
    }

    private void AssignCraftSlots()
    {
        for(int i=0;i<CraftSlotParent.childCount;i++)
        {
            craftSlots.Add(CraftSlotParent.GetChild(i).GetComponent<UI_CraftSlot>());
        }
    }
    public void   SetupCraftList()
    {
        for(int i=0;i<craftSlots.Count;i++)
        {
            Destroy(craftSlots[i].gameObject);
        }
        craftSlots = new List<UI_CraftSlot>();
        for(int i=0;i<craftEquirment.Count;i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, CraftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetUpCraftSlot(craftEquirment[i]);
        }
    }
    // Update is called once per frame

    public void OnPointerDown(PointerEventData eventData)
    {
        SetupCraftList();
    }
}
