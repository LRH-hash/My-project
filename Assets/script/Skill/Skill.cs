using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldowntime;
    public float cooldowntimer;
    public Player player;
    private void Awake()
    {
        player = PlayerManager.instance.player;
    }
    public  virtual void Start()
    {
        
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
        return false;
    }
    public virtual void UseSkill()
    {

    }
}
