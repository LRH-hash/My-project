using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public enum itemType
{
    Material,
    Equipment
}
[CreateAssetMenu]
public class itemData :ScriptableObject
{
    public string itemname;
    public Sprite icon;
    public itemType itemtype;
    [Range(0, 100)]
    public int dropchance;
    public StringBuilder sb = new StringBuilder();
    // Start is called before the first frame update
    public virtual string GetDescription()
    {
        return "";
    }
}
