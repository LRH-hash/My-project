using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactState
{
    public Player player;
    public override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
    }

    public override void Takedamage(int _damage)
    {
        base.Takedamage(_damage);
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        player.StateMachine.ChangeState(player.diestate);
        isDie = true;
        drop.GenerateDrop();
    }
    public override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);
        if(currentHP<=GetHealthHP()*0.2f)
        {
                Inventory.Instance.UseArmor();
        }
    }
}
