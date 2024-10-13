using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,ISaveManager
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
    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UnlockSkillSlot);
    }
    void Start()
    {
        uI = GetComponentInParent<UI>();
        skillImage = GetComponent<Image>();
        skillImage.color = lockColor;
        if (unlock)
            skillImage.color = Color.white;
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
    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(SkillName, out bool value))
        {
            unlock = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.skillTree.TryGetValue(SkillName, out bool value))//这应该是跟clear一样的，但是为什么不直接用clear？
                                                                   //因为clear会调用24次，每一次都把前面保存的删了，最后只剩下一个
        {
            _data.skillTree.Remove(SkillName);
            _data.skillTree.Add(SkillName, unlock);
        }
        else
        {
            _data.skillTree.Add(SkillName, unlock);
        }
        //_data.skillTree.Clear();
        //_data.skillTree.Add(skillName, unlocked);

    }
}

