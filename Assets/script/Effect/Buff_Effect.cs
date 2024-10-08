using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Buff_effect")]
public class Buff_Effect : Itemeffect
{
    public int value;
    public float BuffDuringTime;
    public StatsType statsType;
    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stats.IncreasaeStatBy(value, BuffDuringTime, stats.GetModifyType(statsType));
    }

}

