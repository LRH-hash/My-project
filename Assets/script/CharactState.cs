using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatsType
{
    maxHP,
    Strength,  //力量
    agality, //敏捷值
    intelligence,//1智力和3魔法抵抗；
    vatility, //生命
    armor,  //护甲
    evasion, //闪避率
    Damage,
    critchance,//暴击率
    critPower,//爆伤
    MagicResistence,
    fireDamage,
    iceDamage,
    lightDamage
}
public class CharactState : MonoBehaviour
{
   [Header("Major")]
    public Stat maxHP;
    public int currentHP;
    public Stat Strength;  //力量
    public Stat agality;  //敏捷值
    public Stat intelligence;//1智力和3魔法抵抗；
    public Stat vatility; //生命
    public Stat armor;   //护甲
    public Stat evasion;  //闪避率
    public Stat Damage;
    public Stat critchance;//暴击率
    public Stat critPower;//爆伤
    public Stat MagicResistence;
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightDamage;
    public bool isIgnited;
    public bool isChilled;
    public bool isshocked;
    public float FireDamageCoolDownTime=.3f;
    public float FireDamageTimer;
    public float Ignitedtimer;
    private float chilltimer;
    private float lighttimer;
    public int IgnitedDamage;
    public hitFX Fx;
    public float slowPercent = 0.2f;
    public int ThunderDamage;
    public GameObject ThunderPrefab;
    public bool isDie = false;
    public ItemDrop drop;
    public bool Invincible;
    public int totalDamage;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Fx = GetComponent<hitFX>();
        critPower.SetDefaultValue(150);
        currentHP = GetHealthHP();
        drop = GetComponent<ItemDrop>();
    }
    public System.Action UpHealth;
    // Update is called once per frame
    public virtual void Update()
    {
        FireDamageTimer -= Time.deltaTime;
        Ignitedtimer -= Time.deltaTime;
        chilltimer -= Time.deltaTime;
        lighttimer -= Time.deltaTime;
        if (chilltimer < 0)
            isChilled = false;
        if (lighttimer < 0)
            isshocked = false;
        if (Ignitedtimer < 0)
            isIgnited = false;
        if(FireDamageTimer<0&&isIgnited)
        {
            FireDamageTimer = FireDamageCoolDownTime;
            DecreaseHealthBy(IgnitedDamage);
            Fx.creatText(IgnitedDamage.ToString(), Color.red);
            if (currentHP < 0&&isDie)
                Die();
        }
    }
    public virtual void IncreasaeStatBy(int _modify,float _DuringTime,Stat _stateModify)
    {
        StartCoroutine(StatModCoroutine(_modify, _DuringTime, _stateModify));
    }
    public virtual IEnumerator StatModCoroutine(int _modify,float _DuringgTime,Stat _stateModify)
    {
        _stateModify.AddModify(_modify);
        yield return new WaitForSeconds(_DuringgTime);
        _stateModify.RemoveModify(_modify);
    }
    public void ApplyAllment(bool _isignited,bool _ischilled,bool _isshocked)
    {
        bool canApplyignited = !isChilled && !isIgnited && !isshocked;
        bool canChilled = !isChilled && !isIgnited && !isshocked;
        bool canShocked = !isChilled&& !isIgnited;
        if (_isignited&& canApplyignited)
        {
            isIgnited = _isignited;
            Ignitedtimer = 4;
            Fx.IgniteFor(4);
        }
        if (_ischilled&& canChilled)
        {
            isChilled = _ischilled;
            chilltimer = 4;
            Fx.ChillFor(4);
            GetComponent<entity>().SlowEntry(slowPercent,4);
        }
        if (_isshocked&&canShocked)
        {
            if (!isshocked)
            {
                ApplyShock(_isshocked);
            }
            else
            {
                ThunderAttack();

            }
        }
    }

    public void ThunderAttack()
    {
        /* if (GetComponent<Player>() != null)
             return;*/
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);
        float enemyDirection = Mathf.Infinity;
        Transform closeEnemey = null;
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null && Vector2.Distance(i.transform.position, transform.position) > 1f)
            {
                float distance = Vector2.Distance(transform.position, i.transform.position);
                if (distance < enemyDirection)
                {
                    enemyDirection = distance;
                    closeEnemey = i.transform;
                }
            }
        }
        if (closeEnemey == null)
            closeEnemey = transform;
        if (closeEnemey != null)
        {
            GameObject go = Instantiate(ThunderPrefab, transform.position, ThunderPrefab.transform.rotation);
            go.GetComponent<Thunder_Controller>().SetThunder(ThunderDamage, closeEnemey.GetComponent<CharactState>());
        }
    }

    public void ApplyShock(bool _isshocked)
    {
        if (isshocked)
            return;
        isshocked = _isshocked;
        lighttimer = 4;
        Fx.ShockFor(4);
    }

    public void MagicDamage(CharactState stat,Transform magicDirection)
    {
        stat.GetComponent<entity>().SetKnockDirection(magicDirection);
        int _FireDamage = fireDamage.GetValue();
        int _IceDamage = iceDamage.GetValue();
        int _lightDamage = lightDamage.GetValue();
        int totalMagicDamage = CheckMagicResistence(stat, _FireDamage, _IceDamage, _lightDamage);
        if (Mathf.Max(_FireDamage, _IceDamage, _lightDamage) <= 0)
            return;
        bool canApplyignited = _FireDamage > _IceDamage && _FireDamage > _lightDamage;
        bool canChilled = _IceDamage > _FireDamage && _IceDamage > _lightDamage;
        bool canShocked = _lightDamage > _FireDamage && _lightDamage > _IceDamage;
        stat.Takedamage(totalMagicDamage);
        if (canApplyignited)
        {
            stat.Fx.creatText(totalMagicDamage.ToString(), Color.red);
        }
        else if (canChilled)
        {
            stat.Fx.creatText(totalMagicDamage.ToString(), Color.blue);
        }
        else if (canShocked)
        {
            stat.Fx.creatText(totalMagicDamage.ToString(), Color.yellow);
        }
        else
        {
            stat.Fx.creatText(totalMagicDamage.ToString());
        }

        while (!canApplyignited&&!canChilled&&!canShocked)
        {
            switch(Random.Range(0,3))
            {
                case 0:
                    if (_FireDamage > 0)
                        canApplyignited = true;
                    break;
                case 1:
                    if (_IceDamage > 0)
                        canChilled = true;
                    break;
                case 2:if (_lightDamage > 0)
                        canShocked = true;
                    break;
            }
        }
        if (canApplyignited)
        {          
            stat.IgnitedDamage = (int)(_FireDamage * 0.2f);
        }
        if (canShocked&&stat.isshocked)
        stat.ThunderDamage =(int)(lightDamage.GetValue() * 0.5f);
        stat.ApplyAllment(canApplyignited, canChilled, canShocked);
    }

   public virtual int CheckMagicResistence(CharactState stat, int FireDamage, int IceDamage, int _lightDamage)
    {
        int totalMagicDamage = FireDamage + IceDamage + _lightDamage + intelligence.GetValue();
        totalMagicDamage -= (stat.MagicResistence.GetValue() + (stat.intelligence.GetValue() * 3));
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    public virtual void Dodamage(CharactState _stat,Transform attackPosition)
    {
        bool critical = false;
        if (Invincible)
            return;
        if (CanAvoidAttack(_stat))
            return;
        _stat.GetComponent<entity>().SetKnockDirection(attackPosition);
        int totalDamage = Damage.GetValue() + Strength.GetValue();
        if (canCrit())
        {
            totalDamage = calculateCriticalDamge(totalDamage);
            critical = true;
        }
        Fx.CreateHitFx(_stat.transform,critical);
      totalDamage = CheckArmor(_stat, totalDamage);
        _stat.Fx.creatText(totalDamage.ToString());
        _stat.Takedamage(totalDamage);
    }

    public virtual int CheckArmor(CharactState _stat, int totalDamage)
    {
        if (isChilled)
        {
            totalDamage -= (int)(_stat.armor.GetValue() * 0.8f);
        }
        else
        {
            totalDamage -= _stat.armor.GetValue();
        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
   public virtual void OnEvasion()
    {
    }
   public virtual bool CanAvoidAttack(CharactState _stat)
    {
        int Avoidtotal = _stat.agality.GetValue() + _stat.evasion.GetValue();
        if (_stat.isshocked)
            Avoidtotal -= 20;
        if (Random.Range(0, 100) < Avoidtotal)
        {
            _stat.OnEvasion();
            return true;
        }
        else
            return false;
    }

    public virtual void Takedamage(int _damage)
    {
        DecreaseHealthBy(_damage);
        GetComponent<entity>().damageEffect();
        if (currentHP <= 0&&!isDie)
            Die();
    }
    public virtual void IncreaseHealthBy(int _damage)
    {

        currentHP += _damage;
        if(currentHP>GetHealthHP())
        {
            currentHP = GetHealthHP();
        }
        UpHealth?.Invoke();
    }
    public virtual void DecreaseHealthBy(int _damage)
    {
        currentHP -= _damage;
        UpHealth?.Invoke();
    }
    public virtual int calculateCriticalDamge(int _damage)
    {
        float totalcritPower = (critPower.GetValue() + Strength.GetValue()) * 0.01f;
        float totalDamage = _damage * totalcritPower;
        return (int)totalDamage;
    }
    public bool canCrit()
    {
        int totalCrit = critchance.GetValue() + agality.GetValue();
        if (Random.Range(0,100)<totalCrit)
            return true;
        else
            return false;
    }
    public virtual void Die()
    {
       
    }
    public int GetHealthHP()
    {
        return maxHP.GetValue() + (Strength.GetValue() * 5)+vatility.GetValue();
    }
    public Stat GetModifyType(StatsType statsType)
    {
        if (statsType == StatsType.maxHP) return maxHP;
        if (statsType == StatsType.Strength) return Strength;
        else if (statsType == StatsType.agality) return agality;
        else if (statsType == StatsType.intelligence) return intelligence;
        else if (statsType == StatsType.vatility) return vatility;
        else if (statsType == StatsType.armor) return armor;
        else if (statsType == StatsType.evasion) return evasion;
        else if (statsType == StatsType.Damage) return Damage;
        else if (statsType == StatsType.critchance) return critchance;
        else if (statsType == StatsType.critPower) return critPower;
        else if (statsType == StatsType.MagicResistence) return MagicResistence;
        else if (statsType == StatsType.fireDamage) return fireDamage;
        else if (statsType == StatsType.iceDamage) return iceDamage;
        else if (statsType == StatsType.lightDamage) return lightDamage;
        return null;
    }
    public void MakeInvincible(bool _invincible) => Invincible = _invincible;
}
