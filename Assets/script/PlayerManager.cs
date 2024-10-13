using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;
    public int currentSouls;
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    public bool spendSouls(int _souls)
    {
        if (currentSouls < _souls)
            return false;
        else
            currentSouls -= _souls;
        return true;
    }
    public int GetCurrentAmount() => currentSouls;
 
    public void LoadData(GameData _data)
    {
        currentSouls = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currentSouls;
    }
}

