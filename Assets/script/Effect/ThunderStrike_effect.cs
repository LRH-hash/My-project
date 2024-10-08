using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/ThunderStrike_effect")]
public class ThunderStrike_effect : Itemeffect
{
    public GameObject ThunderPerfab;
    // Start is called before the first frame update

    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
        GameObject go=Instantiate(ThunderPerfab,_enemyTransform.position,Quaternion.identity);
        Destroy(go, 0.5f);
    }
}
