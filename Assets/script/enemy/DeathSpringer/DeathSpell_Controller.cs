using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpell_Controller : MonoBehaviour
{
    public Transform check;
    public Vector2 boxsize;
    public LayerMask whatisPlay;
    public CharactState mystat;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyAttackCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxsize, whatisPlay);
        foreach (var i in colliders)
        {
            if (i.GetComponent<Player>() != null)
            {
                mystat.Dodamage(i.GetComponent<PlayerStats>(), transform);
            }
        }
    }
    public void selfDestory() => Destroy(gameObject);
    public void setspell(CharactState _stat) => mystat = _stat;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(check.position, boxsize);
    }
}
