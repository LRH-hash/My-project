using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
    public string itemId;
    [Range(0, 100)]
    public int dropchance;
    public StringBuilder sb = new StringBuilder();
    // Start is called before the first frame update
    public virtual string GetDescription()
    {
        return "";
    }

    private void OnValidate()
    {
        //给每个item随机分配一个id
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }


}

