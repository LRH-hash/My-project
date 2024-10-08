using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemToolTip : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI typetext;
    public TextMeshProUGUI Desrcription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTip(ItemData_equirment item)
    {
        if (item == null)
            return;
        nametext.text = item.itemname;
        typetext.text = item.itemtype.ToString();
        Desrcription.text = item.GetDescription();
        gameObject.SetActive(true);
    }
    public void HideTip() => gameObject.SetActive(false);
}
