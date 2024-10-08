using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatToolTip : MonoBehaviour
{
    public TextMeshProUGUI Desrcription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTip(UI_StatSlot stat)
    {
        if (stat == null)
            return;
        Desrcription.text = stat.Decrpiatinon;
    
        gameObject.SetActive(true);
    }
    public void HideTip() => gameObject.SetActive(false);
}
