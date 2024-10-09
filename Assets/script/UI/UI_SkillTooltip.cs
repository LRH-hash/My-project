using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_SkillTooltip : MonoBehaviour
{
    public TextMeshProUGUI skilltext;
    public void showTip(string _text)
    {
        skilltext.text = _text;
        gameObject.SetActive(true);
    }
    public void hideTip() => gameObject.SetActive(false);
}
