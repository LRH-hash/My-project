using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyrstal_Skill : Skill
{
    public GameObject CryStalPrefab;
    public GameObject crystal;
    public float exititme;
    public float movespeed;
    public bool canmove;
    public bool canexpolosive;
    public bool useskill = true;
    public List<GameObject> cyrstalList;
    public float amountAttack;
    public float Cyrstalcooldown;
    public float auto_completetime;
    public float cooltimer;
    public bool IsCloneAttack;
    public override void Update()
    {
        cooltimer -= Time.deltaTime;
        base.Update();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (CystalCanUseSkill())
        {
            return;
        }
        if (crystal == null)
        {
            Createcrystal();
        }
        else
        {
            if (canmove)
                return;
            Vector2 posposition = player.transform.position;
            player.transform.position = crystal.transform.position;
            crystal.transform.position = posposition;
            if (IsCloneAttack)
                SkillManager.instance.clone.CreateClone(crystal.transform,Vector2.zero);
        }
    }

    public void Createcrystal()
    {
        crystal = Instantiate(CryStalPrefab, player.transform.position, Quaternion.identity);
        crystal.GetComponent<Crystal_Skill_Controller>().SetCryStal(exititme, canexpolosive, canmove, movespeed, FindEnemy(crystal.transform));
    }
    public void crystalRandomchoose() => crystal.GetComponent<Crystal_Skill_Controller>().chooseTarget();

    public Transform FindEnemy(Transform _transform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position,10f);
        Transform enemy=null;
        float enemyDirection = Mathf.Infinity;
        foreach (var i in colliders)
        { 
            if (i.GetComponent<enemy>() != null)
            {
                float distance = Vector2.Distance(_transform.position, i.transform.position);
                if (distance < enemyDirection)
                {
                    enemyDirection = distance;
                    enemy = i.transform;
                }
            }
        }
        return enemy;
    }
    public bool CystalCanUseSkill()
    {
        if (useskill)
        {
            if (cyrstalList.Count > 0)
            {
                if (amountAttack== cyrstalList.Count)
                {
                    Invoke("AddCrystal", auto_completetime);
                }
                GameObject go =Instantiate(cyrstalList[cyrstalList.Count - 1], player.transform.position, Quaternion.identity);
                
                go.GetComponent<Crystal_Skill_Controller>().SetCryStal(exititme, canexpolosive, canmove, movespeed, FindEnemy(go.transform));
                cyrstalList.Remove((cyrstalList[cyrstalList.Count - 1]));
            }
            else
            {
                if (cyrstalList.Count<=0)
                {
                        AddCrystal();
                    if(cooltimer<0)
                   cooltimer= Cyrstalcooldown;
                }
            }
            return true;
        }
        else
            return false;
        }
        public void AddCrystal()
    {
        if (cooltimer < 0)
        {
            float number = amountAttack - cyrstalList.Count;
            for (int i = 0; i < number; i++)
            {
                cyrstalList.Add(CryStalPrefab);
            }
        }
    }

}
