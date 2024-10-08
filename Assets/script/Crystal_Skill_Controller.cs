using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    public float exitTimer;
    public bool isgrow;
    public bool isexplosive = true;
    public float growSpeed = 5f;
    public bool canmove;
    public float movespeed;
    public Transform enemyTransform;
    public Animator anim => GetComponent<Animator>();
    public CircleCollider2D cd => GetComponent<CircleCollider2D>();
    public LayerMask whatisGround;

    public void Update()
    {
        exitTimer -= Time.deltaTime;
        if (exitTimer < 0&&isexplosive)
        {
            FinishExplosion();
        }
        if (isgrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(2, 2), growSpeed * Time.deltaTime);
        }
        if (canmove&&enemyTransform!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTransform.position, movespeed * Time.deltaTime);
            {
                if (Vector2.Distance(transform.position, enemyTransform.position) < 0.5f)
                {
                    canmove = false;
                    FinishExplosion();

                }

            }
        }
    }

    private void FinishExplosion()
    {
        if (isexplosive)
        {
            isgrow = true;
            anim.SetTrigger("explosive");
            isexplosive = false;
        }
    }
    public void chooseTarget()
    {
        float radius = SkillManager.instance.blackHole.GetRadius();
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, radius,whatisGround);
        if (collider2D != null)
        {
            enemyTransform = collider2D[Random.Range(0, collider2D.Length)].transform;
        }
    }



    public void SetCryStal(float _time,bool _isexplosive,bool _canmove,float _movespeed,Transform _enemy)
    {
        exitTimer = _time;
        isexplosive = _isexplosive;
        canmove = _canmove;
        movespeed = _movespeed;
        enemyTransform = _enemy;
        
    }
    public void DestroyMe() => Destroy(gameObject);
    public void AnimatorTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,cd.radius);
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {
                PlayerManager.instance.player.charactState.MagicDamage(i.GetComponent<CharactState>());
                ItemData_equirment equirment = Inventory.Instance.GetEquipment(equirmentType.Amult);
                if (equirment != null)
                    equirment.ExecuteitemEffect(i.transform);
            }
        }
    }

}
