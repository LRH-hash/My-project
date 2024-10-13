using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Controller : MonoBehaviour
{
    public float CloneTime;
    public SpriteRenderer sc;
    public float colorloosingspeed=1;
    public Transform checkattack;
    public float checkattackRidius = 10f;
    public Animator anim;
    public Transform enemy;
    public bool canDuplicateClone;
    public float changeDulicateClone;
    public float faceDir = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        sc = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        CloneTime -= Time.deltaTime;
        if(CloneTime<0)
        sc.color = new Color(1, 1, 1, sc.color.a - (Time.deltaTime * colorloosingspeed));
        if (sc.color.a < 0)
            Destroy(gameObject);
    }
    public void SetupClone(float DuringTime,bool canattack,bool _canduplication,float _changeDuplication)
    {
        if(canattack)
        anim.SetInteger("attackcombo", Random.Range(1, 4));
        CloneTime = DuringTime;
        EachColiderTarget();
        canDuplicateClone = _canduplication;
        changeDulicateClone = _changeDuplication;
    }
    public void stopattack()
    {
        CloneTime = .1f;
    }
    public void EachColiderTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkattack.position, checkattackRidius);
        float enemyDirection = Mathf.Infinity;
        Transform closeEnemey = null;
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, i.transform.position);
                if(distance<enemyDirection)
                {
                    enemyDirection = distance;
                    closeEnemey=i.transform;
                }
            }
        }
        if(closeEnemey != null)
        {
            if(closeEnemey.position.x<transform.position.x)
            {
                faceDir = -1;
                transform.Rotate(0, 180,0);
            }
        }
    }
    public void PlayerAttackCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkattack.position, PlayerManager.instance.player.checkattackRidius);
        foreach (var i in colliders)
        {
            if (i.GetComponent<enemy>() != null)
            {
                PlayerManager.instance.player.charactState.Dodamage(i.GetComponent<CharactState>(),transform);
                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < changeDulicateClone)
                        SkillManager.instance.clone.CreateClone(i.transform, new Vector2(faceDir, 0));
                }
            }
        }
    }
}
