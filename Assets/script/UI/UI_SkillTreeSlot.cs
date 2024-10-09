using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_SkillTreeSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string SkillName;
    [TextArea]
    public string SkillDescrption;
    public bool unlock;
    public Color lockColor;
    public UI_SkillTreeSlot[] shouldbeunlock;
    public UI_SkillTreeSlot[] shouldbelock;
    public Image skillImage;
    public UI uI;
    public void OnValidate()
    {
        /*gameObject.name = "Skill" + SkillName;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        uI = GetComponentInParent<UI>();
        skillImage = GetComponent<Image>();
        skillImage.color = lockColor;
        GetComponent<Button>().onClick.AddListener(()=>UnlockSkillSlot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnlockSkillSlot()
    {
        for(int i=0;i<shouldbeunlock.Length;i++)
        {
            if (shouldbeunlock[i].unlock == false)
                return;
        }
        for (int i = 0; i < shouldbelock.Length; i++)
        {
            if (shouldbelock[i].unlock == true)
                return;
        }
        skillImage.color = Color.white;
        unlock = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uI.skilltooltip.showTip(SkillDescrption);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uI.skilltooltip.hideTip();
    }
}
