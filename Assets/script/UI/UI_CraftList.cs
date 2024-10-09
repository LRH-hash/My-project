using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour,IPointerDownHandler
{
    public Transform CraftSlotParent;
    public GameObject craftSlotPrefab;
    public List<ItemData_equirment> craftEquirment;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultCraftWindow();
    }

    public void SetupCraftList()
    {
        for (int i = 0; i < CraftSlotParent.childCount; i++)
        {
            Destroy(CraftSlotParent.GetChild(i).gameObject);
        }

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
    public void SetDefaultCraftWindow()
    {
        if (craftEquirment != null)
            GetComponentInParent<UI>().CraftWindow.SetUpCraftWIndow(craftEquirment[0]);
    }
}
