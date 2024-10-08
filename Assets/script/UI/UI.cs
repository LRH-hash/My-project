using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ItemToolTip itemtip;
    public StatToolTip stattip;
    public GameObject skillTree;
    public GameObject character;
    public GameObject Craft;
    public GameObject option;
    public UI_CraftWindow CraftWindow;
    private void Start()
    {
        SwitchTo(null);
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
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if(_menu!=null)
        _menu.SetActive(true);
    }
    public void SwitchWithKeyTO(GameObject _menu)
    {
        if(_menu!=null&&_menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }
        SwitchTo(_menu);
    }
}
