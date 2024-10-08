using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Heal_effect")]
public class Heal_effect : Itemeffect
{
    public float healPercent;
    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        int heal = (int)(playerStats.GetHealthHP() * healPercent);
        playerStats.IncreaseHealthBy(heal);
    }
}
