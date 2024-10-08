using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum equirmentType
{
    Weapon,
    armor,
    Amult,
    Flask
}
[CreateAssetMenu]
public class ItemData_equirment : itemData
{
    // Start is called before the first frame update
    public equirmentType equirmenttype;
    public int Strength;  //力量
    public int agality;  //敏捷值
    public int intelligence;//1智力和3魔法抵抗；
    public int vatility; //生命
    public int armor;   //护甲
    public int evasion;  //闪避率
    public int Damage;
    public int critchance;//暴击率
    public int critPower;//爆伤
    public int MagicResistence;
    public int fireDamage;
    public int iceDamage;
    public int lightDamage;
    public PlayerStats  stat;
    public Itemeffect[] itemeffect;
    public float cooldown = 4;
    public float DescriptionLnegth = 0;
    public List<InventoryItem> CraftingMaterial;
    public void AddModify()
    {
        stat = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stat.Strength.AddModify(Strength);
        stat.agality.AddModify(agality);
        stat.intelligence.AddModify(intelligence);
        stat.vatility.AddModify(vatility);
        stat.armor.AddModify(armor);
        stat.evasion.AddModify(evasion);
        stat.Damage.AddModify(Damage);
        stat.critchance.AddModify(critchance);
        stat.critPower.AddModify(critPower);
        stat.MagicResistence.AddModify(MagicResistence);
        stat.fireDamage.AddModify(fireDamage);
        stat.iceDamage.AddModify(iceDamage);
        stat.lightDamage.AddModify(lightDamage);

    }
    public void RemoveModify()
    {
        stat = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stat.Strength.RemoveModify(Strength);
        stat.agality.RemoveModify(agality);
        stat.intelligence.RemoveModify(intelligence);
        stat.vatility.RemoveModify(vatility);
        stat.armor.RemoveModify(armor);
        stat.evasion.RemoveModify(evasion);
        stat.Damage.RemoveModify(Damage);
        stat.critchance.RemoveModify(critchance);
        stat.critPower.RemoveModify(critPower);
        stat.MagicResistence.RemoveModify(MagicResistence);
        stat.fireDamage.RemoveModify(fireDamage);
        stat.iceDamage.RemoveModify(iceDamage);
        stat.lightDamage.RemoveModify(lightDamage);
    }
    public void ExecuteitemEffect(Transform _enemyTrasnform)
    {
         foreach(var i in itemeffect)
        {
            i.ExecuteitemEffect(_enemyTrasnform);
        }
    }
    public override string GetDescription()
    {
        sb.Length = 0;
        DescriptionLnegth = 0;
        AddItemDescription(Strength, "Strength");
        AddItemDescription(agality, "agality");
        AddItemDescription(intelligence, "intelligence");
        AddItemDescription(vatility, "vatility");
        AddItemDescription(armor, "armor");
        AddItemDescription(evasion, "evasion");
        AddItemDescription(Damage, "Damage");
        AddItemDescription(critchance, "critchance");
        AddItemDescription(critPower, "critPower");
        AddItemDescription(MagicResistence, "MagicResistence");
        AddItemDescription(fireDamage, "fireDamage");
        AddItemDescription(iceDamage, "iceDamage");
        AddItemDescription(lightDamage, "lightDamage");
        if (DescriptionLnegth < 5)
        {
            for (int i = 0; i < 5 - DescriptionLnegth; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }
        return sb.ToString();
    }
    public void AddItemDescription(int _value,string _name)
    {
        if(_value!=0)
        {
            if (sb.Length > 0)
                sb.AppendLine();
            if (_value > 0)
                sb.Append("+"+_name + ":" + _value);
            DescriptionLnegth++;
        }
    }
}
