using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactState
{
    public Player player;
    public Vector2 damageDistance;
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
        if (isDie == false)
            drop.GenerateDrop();
        isDie = true;
    }
    public override void DecreaseHealthBy(int _damage)
    {
        if (_damage >= GetHealthHP() * 0.3f)
        {
            player.SetupKnockPower(damageDistance);
            player.fX.ScreemShake(player.fX.shakeDamage);
        }
        base.DecreaseHealthBy(_damage);
        if(currentHP<=GetHealthHP()*0.2f)
        {
              Inventory.Instance.UseArmor();
        }

    }
    public override void OnEvasion()
    {
        SkillManager.instance.Dogge.CreateMirageOnDoDogge();
    }
}
