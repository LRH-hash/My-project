using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI : MonoBehaviour
{
    public static UI instance { get; private set; }
    public ItemToolTip itemtip;
    public StatToolTip stattip;
    public GameObject skillTree;
    public GameObject character;
    public GameObject Craft;
    public GameObject option;
    public UI_CraftWindow CraftWindow;
    public UI_SkillTooltip skilltooltip;
    public GameObject InGameUI;
    public GameObject FadeUI;
    public GameObject die;
    public GameObject tryAgainButton;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        FadeUI.SetActive(true);
        SwitchTo(InGameUI);
        itemtip.gameObject.SetActive(false);
        stattip.gameObject.SetActive(false);
   
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWithKeyTO(character);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchWithKeyTO(skillTree);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchWithKeyTO(Craft);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchWithKeyTO(option);
        }
    }
    // Start is called before the first frame update

    public void SwitchTo(GameObject _menu)
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).GetComponent<UI_FadeScene>()==false)
             transform.GetChild(i).gameObject.SetActive(false);
        }
        if(_menu!=null)
        _menu.SetActive(true);
        if (GameManager.instance != null)
        {
            if (_menu == InGameUI)
            {
                GameManager.instance.GamePaUse(false);
            }
            else
            {
                GameManager.instance.GamePaUse(true);
            }
        }
    }
    public void SwitchWithKeyTO(GameObject _menu)
    {
        if(_menu!=null&&_menu.activeSelf)
        {
            _menu.SetActive(false);
            itemtip.HideTip();
            stattip.HideTip();
            SwitchTo(InGameUI);
            return;
        }

        SwitchTo(_menu);
    }
    public void DieSwitch()
    {
        FadeUI.GetComponent<UI_FadeScene>().FadeOut();
        StartCoroutine("Die");    
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1.2f);
        die.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        tryAgainButton.SetActive(true);
    }
    public void TryAgain() => GameManager.instance.GameAgain();
}
