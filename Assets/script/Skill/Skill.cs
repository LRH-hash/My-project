using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldowntime;
    public float cooldowntimer;
    public Player player;
    public  virtual void Start()
    {
        player = PlayerManager.instance.player;
        CheckUnlock();
    }
    public virtual void Update()
    {
        cooldowntimer -=Time.deltaTime;
    }
    public virtual bool CanUseSkill()
    {
        if (cooldowntimer<0)
        {
            UseSkill();
            cooldowntimer = cooldowntime;
            return true;
        }
        player.fX.creatText("cooldown");
        return false;
    }
    public virtual void UseSkill()
    {

    }
    public virtual void CheckUnlock()
    {

    }
}
