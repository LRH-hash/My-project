using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharactState
{
    public enemy enemy;
    public int level;
    [Range(0, 1)]
    public float Percentage = 0.3f;
    public Stat enemySoulAmount;

    // Start is called before the first frame update
    public override void Start()
    {
        enemySoulAmount.SetDefaultValue(100);
        ApplyLevelModify();
        base.Start();
        enemy = GetComponent<enemy>();
    }

    private void ApplyLevelModify()
    {
        Modify(maxHP);
        Modify(Damage);
        Modify(critchance);
        Modify(critPower);
        Modify(MagicResistence);
        Modify(enemySoulAmount);
        //下方待选，数值膨胀；
  /*      Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightDamage);
        Modify(Strength);
        Modify(intelligence);
        Modify(evasion);
        Modify(vatility);
        Modify(armor);*/
    }

    public void Modify(Stat _stat)
    {
        for(int i=1;i<level;i++)
        {
            float modify = _stat.GetValue() * Percentage;
            _stat.AddModify((int)modify);
        }
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
        enemy.Die();
       if(isDie==false)
        drop.GenerateDrop();
        enemy.isDie = true;
        isDie = true;
        PlayerManager.instance.currentSouls += enemySoulAmount.GetValue();
    }
}
