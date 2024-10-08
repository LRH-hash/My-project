using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat 
{
   
    public int baseValue;
    public List<int> modify;
    
    public int GetValue()
    {
        int Finvalue = baseValue;
        foreach(int i in modify)
        {
            Finvalue += i;
        }
        return Finvalue;
    }
    public void AddModify(int _modify)
    {
        modify.Add(_modify);
    }
    public void RemoveModify(int _modify)
    {
        modify.Remove(_modify);
    }
    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

}
