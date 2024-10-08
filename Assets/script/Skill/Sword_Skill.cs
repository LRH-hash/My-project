using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill :Skill
{  
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;//sword预制体
    [SerializeField] private Vector2 launchForce;//发射力度
    [SerializeField] private float swordGravity;//发射体的重力
    public float bounceGravity;
    public float bounceMaxCount;
    public int PierceMaxCount;
    public float PierceGravity;
    public SwordType swordType=SwordType.Regular;
    private Vector2 finalDir;//发射方向
    public float FreezeTime;
    private float changeState;
  [Header("Aim dots")]
    [SerializeField] private int numberOfDots;//需要的点的数量
    [SerializeField] private float spaceBetweenDots;//相隔的距离
    [SerializeField] private GameObject dotPrefab;//dot预制体
    [SerializeField] private Transform dotsParent;//不是很懂
    [Header("Spin info")]
    public float spinMaxDisntance;
    public float spinDuringTime;
    public float spinGravity;
    public float hittimer;
    private GameObject[] dots;//dot组

    public override void Start()
    {
        base.Start();

        GenerateDots();//生成点函数
    }
    public override  void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
            //将位移量改为单位向量分别与力度的x，y相乘作为finalDir

        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);//用循环为每个点以返回值赋值（传入值为每个点的顺序i*点间距
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            swordType = SwordType.Regular;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        { 
            swordType = SwordType.Bounce;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            swordType = SwordType.Pierce;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            swordType = SwordType.Spin;
        }
        switch (swordType)
        {
            case SwordType.Regular:
                break;
            case SwordType.Bounce:
                swordGravity = bounceGravity;
                break;
            case SwordType.Pierce:
                swordGravity = PierceGravity;
                break;
            case SwordType.Spin:
                swordGravity = spinGravity;
                break;
        }
    }
 
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, dotsParent.transform.position, transform.rotation);//创造实例，初始位置为此时player的位置
        Sword_SkillController newSwordScript = newSword.GetComponent<Sword_SkillController>();
        if (swordType == SwordType.Bounce)
        {
            newSwordScript.BounceSet(true, bounceMaxCount);
        }
        else if(swordType==SwordType.Pierce)
        {
            newSwordScript.PierceSet(PierceMaxCount);
        }
        else if(swordType==SwordType.Spin)
        {
            newSwordScript.SpinSet(true, spinMaxDisntance, spinDuringTime, hittimer);
        }
        newSwordScript.SetupSword(finalDir, swordGravity,player,FreezeTime);//调用Controller里的SetupSword函数，给予其速度和重力
        player.GetSword(newSword);
        DotsActive(false);//关闭点的显示
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;//拿到玩家此时的位置
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//大概就是返回屏幕上括号里的参数的位置，这里回了鼠标的位置
 //拿到此时鼠标的位置
        Vector2 direction = mousePosition - playerPosition;//获得距离的绝对向量

        return direction;//返回距离向量
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);//设置每个点是否显示函数
        }
    }
    private void GenerateDots()//生成点函数
    {
        dots = new GameObject[numberOfDots];//为dot赋予实例数量
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);//对象与世界轴或父轴完全对齐
  
            dots[i].SetActive(false);//关闭dot
        }
    }

    private Vector2 DotsPosition(float t)//传入顺序相关的点间距
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2
            (AimDirection().normalized.x * launchForce.x,
             AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);
        //t是控制之间点间距的
        return position;//返回位置
    }//设置点间距函数

}
