using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BlackHole : Skill
{
    public int amountOfAttacks = 4;
    public float cloneAttackCooldown = .3f;
    public float maxSize;//���ߴ�
    public float growSpeed;//����ٶ�
    public bool canGrow;//�Ƿ���Ա��
    public float shrinkSpeed;//��С�ٶ�
    public bool canShrink;
    public GameObject blackholeprefab;
    public Blackhole_Skill_Controller blackhole;
    public float blackDurintTime;
    // Start is called before the first frame update
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject hole = GameObject.Instantiate(blackholeprefab, player.transform.position, Quaternion.identity);
        hole.GetComponent<Blackhole_Skill_Controller>().SetBlackhole(true, maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneAttackCooldown,blackDurintTime);
    }
    public float GetRadius()
    {
        return maxSize / 2;
    }
}
