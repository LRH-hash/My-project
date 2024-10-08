using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/iceandFireeffect")]
public class IceandFire_effect :Itemeffect
{
    public GameObject iceandFirePrefab;
    public float Xspeed;

    public override void ExecuteitemEffect(Transform _enemyTransform)
    {
         Player  player = PlayerManager.instance.player;
     /*   bool thirdAttack = PlayerManager.instance.player.primaryattackstate.attackcombo ==1;*///第三次攻击保留
        GameObject iceandFire = Instantiate(iceandFirePrefab, _enemyTransform.position, player.transform.rotation);
        iceandFire.GetComponent<Rigidbody2D>().velocity = new Vector2(player.moveRight * Xspeed, 0);
        Destroy(iceandFire, 3);
    }
}
