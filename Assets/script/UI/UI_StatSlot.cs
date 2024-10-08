using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string text;
    public TextMeshProUGUI valuetext;
    public TextMeshProUGUI nametext;
    public StatsType statsType;
    [TextArea]
    public string Decrpiatinon;
    public UI ui;
    //待定，鼠标移至属性显示作用/
    // Start is called before the first frame update
    public void OnValidate()
    {
        gameObject.name = "Stats-" + text;
        if (nametext != null)
            nametext.text = text.ToString();
    }

    // Update is called once per frame
    private void Start()
    {
        UpdataStatValueString();
        ui = GetComponentInParent<UI>();
    }
    public void UpdataStatValueString()
    {
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            valuetext.text = stats.GetModifyType(statsType).GetValue().ToString();
            if(statsType==StatsType.maxHP)
            {
                valuetext.text = stats.GetHealthHP().ToString();
            }
            if (statsType == StatsType.Damage)
            {
                valuetext.text = (stats.Damage.GetValue() + stats.Strength.GetValue()).ToString();
            }
            if(statsType == StatsType.critPower)
            {
                valuetext.text = (stats.critPower.GetValue() + stats.Strength.GetValue()).ToString();
            }
            if (statsType == StatsType.critchance)
            {
                valuetext.text = (stats.critchance.GetValue() + stats.agality.GetValue()).ToString();
            }
            if (statsType == StatsType.evasion)
            {
                valuetext.text = (stats.evasion.GetValue() + stats.agality.GetValue()).ToString();
            }
            if (statsType == StatsType.MagicResistence)
            {
                valuetext.text = (stats.MagicResistence.GetValue() + stats.intelligence.GetValue()*3).ToString();
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.stattip.ShowTip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.stattip.HideTip();
    }
}
