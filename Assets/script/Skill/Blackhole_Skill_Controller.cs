using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> KeyCodeList;

    private float maxSize;//最大尺寸
    private float growSpeed;//变大速度
    private bool canGrow;//是否可以变大
    private float shrinkSpeed;//缩小速度
    private bool canShrink=false;//缩小

    private bool canCreateHotKeys = true;
    private bool cloneAttackReleased;
    private int amountOfAttacks = 4;
    public float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    public float blackDuringTime;
    public bool istransparents =true;
 /*   public bool iscystal;
    public bool isSetParent=false*/
    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] private List<GameObject> createdHotKey = new List<GameObject>();

    public void SetBlackhole(bool _canGrow, float _maxSize, float _growSpeed, float _shirinkSpeed, int _amountofattack, float _cloneAttackCoolDown,float _blackDuringTIme)
    {
        canGrow = _canGrow;
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shirinkSpeed;
        amountOfAttacks = _amountofattack;
        cloneAttackCooldown = _cloneAttackCoolDown;
        blackDuringTime = _blackDuringTIme;
    }
   
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackDuringTime -= Time.deltaTime;
        if(blackDuringTime<0)
        {
            blackDuringTime = Mathf.Infinity;
            if (targets.Count <= 0)
            {
                ExitSkill();
                return;
            }
            else
            {
                RleaseAttackDown();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RleaseAttackDown();
        }

        if (cloneAttackTimer < 0 && cloneAttackReleased&&amountOfAttacks>0)
        {
            if(targets.Count<=0)
            {
                ExitSkill();
                return;
            }
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);


            //限制攻击次数和设置攻击偏移量
            float _offset;

            if (Random.Range(0, 100) > 50)
                _offset = 1;
            else
                _offset = -1;
            if (SkillManager.instance.clone.crystalinstanceClone)
            {
                SkillManager.instance.cyrstal.Createcrystal();
                SkillManager.instance.cyrstal.crystalRandomchoose();
            }
            else
            {
                SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(_offset, 0, 0));
            }
            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                Invoke("ExitSkill", 1f);
            }
        }
        if (canGrow && !canShrink)
        {
            //这是控制物体大小的参数
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            //类似MoveToward，不过是放大到多少大小 
        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0.3f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void RleaseAttackDown()
    {
        cloneAttackReleased = true;
        canCreateHotKeys = false;
        DestroyHotKeys();
        if (istransparents)
        {
            if(!SkillManager.instance.clone.crystalinstanceClone)
            PlayerManager.instance.player.Settransparent(true);
            istransparents = false;
        }
    }

    private void ExitSkill()
    {
        DestroyHotKeys();
        PlayerManager.instance.player.ExitBlackHole();
        canShrink = true;
        cloneAttackReleased = false;
    }

    public void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<enemy>()?.FreezeTimer(false);
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<enemy>() != null)
        {
            collision.GetComponent<enemy>().FreezeTimer(true);
            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (KeyCodeList.Count == 0)//当所有的KeyCode都被去除，就不在创建实例
        {
            return;
        }

        if (!canCreateHotKeys)//这是当角色已经开大了，不在创建实例
        {
            return;
        }

        //创建实例
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        //将实例添加进列表
        createdHotKey.Add(newHotKey);


        //随机KeyCode传给HotKey，并且传过去一个毁掉一个
        KeyCode choosenKey = KeyCodeList[Random.Range(0, KeyCodeList.Count)];

        KeyCodeList.Remove(choosenKey);

       Blackhole_HotKey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKey_Controller>();

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }
    public void AddEnemyToList(Transform _myEnemy)
    {
        targets.Add(_myEnemy);
    }

    //销毁Hotkey
    private void DestroyHotKeys()
    {

        if (createdHotKey.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }

    }
}