using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Freeze_enemies")]
public class Freeze_enemies_effect : Itemeffect
{
    public float DuringTIme;
    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemyTransform.position, 2);
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {
                i.GetComponent<enemy>().SetFreezeTime(DuringTIme);
            }
        }
    }
}

