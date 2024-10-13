using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Clone : Skill
{
    public GameObject Clone;
    public float clonetime=1;
    public bool canattack = true;
    public bool CreateCloneDashStart;
    public bool CreateCloneDashend;
    public bool createCloneOnCounterattack;
    public bool canDuplicateClone;
    public float changeDulicateClone;
    public bool crystalinstanceClone;
    public void CreateClone(Transform _transform,Vector3 offset)
    {    
        if(crystalinstanceClone)
        {
            SkillManager.instance.cyrstal.Createcrystal();
            return;
        }
        GameObject clone =GameObject.Instantiate(Clone);
        clone.transform.position =_transform.position+offset;
        clone.GetComponent<Clone_Controller>().SetupClone(cooldowntime,canattack,canDuplicateClone,changeDulicateClone);
    }
    public void CreateCloneOnDashStart()
    {
        if(CreateCloneDashStart)
        {
            CreateClone(player.transform, Vector2.zero);
        }
    }
    public void CreateCloneOnDashEnd()
    {
        if (CreateCloneDashend)
        {
            CreateClone(player.transform, Vector2.zero);
        }
    }
    public void CreateClonecounterAttack(Transform _transform)
    {
            StartCoroutine("CounterAttackDelay", _transform);
    }
    public IEnumerator CounterAttackDelay(Transform _transform)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, new Vector2(player.moveRight,0));
    }
}
