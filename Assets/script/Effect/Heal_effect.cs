using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Heal_effect")]
public class Heal_effect : Itemeffect
{
    public float healPercent;
    public GameObject healfx;
    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        GameObject healeffect =Instantiate(healfx);
        healeffect.transform.SetParent(PlayerManager.instance.player.transform,false);
        healeffect.transform.localPosition = new Vector2(0, -1); ;
        Destroy(healeffect, 2);
        int heal = (int)(playerStats.GetHealthHP() * healPercent);
        playerStats.IncreaseHealthBy(heal);
    }
}
