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
    [SerializeField] private GameObject swordPrefab;//swordԤ����
    [SerializeField] private Vector2 launchForce;//��������
    [SerializeField] private float swordGravity;//�����������
    public float bounceGravity;
    public float bounceMaxCount;
    public int PierceMaxCount;
    public float PierceGravity;
    public SwordType swordType=SwordType.Regular;
    private Vector2 finalDir;//���䷽��
    public float FreezeTime;
    private float changeState;
  [Header("Aim dots")]
    [SerializeField] private int numberOfDots;//��Ҫ�ĵ������
    [SerializeField] private float spaceBetweenDots;//����ľ���
    [SerializeField] private GameObject dotPrefab;//dotԤ����
    [SerializeField] private Transform dotsParent;//���Ǻܶ�
    [Header("Spin info")]
    public float spinMaxDisntance;
    public float spinDuringTime;
    public float spinGravity;
    public float hittimer;
    private GameObject[] dots;//dot��

    public override void Start()
    {
        base.Start();

        GenerateDots();//���ɵ㺯��
    }
    public override  void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
            //��λ������Ϊ��λ�����ֱ������ȵ�x��y�����ΪfinalDir

        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);//��ѭ��Ϊÿ�����Է���ֵ��ֵ������ֵΪÿ�����˳��i*����
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
        GameObject newSword = Instantiate(swordPrefab, dotsParent.transform.position, transform.rotation);//����ʵ������ʼλ��Ϊ��ʱplayer��λ��
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
        newSwordScript.SetupSword(finalDir, swordGravity,player,FreezeTime);//����Controller���SetupSword�������������ٶȺ�����
        player.GetSword(newSword);
        DotsActive(false);//�رյ����ʾ
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;//�õ���Ҵ�ʱ��λ��
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//��ž��Ƿ�����Ļ��������Ĳ�����λ�ã������������λ��
 //�õ���ʱ����λ��
        Vector2 direction = mousePosition - playerPosition;//��þ���ľ�������

        return direction;//���ؾ�������
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);//����ÿ�����Ƿ���ʾ����
        }
    }
    private void GenerateDots()//���ɵ㺯��
    {
        dots = new GameObject[numberOfDots];//Ϊdot����ʵ������
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);//�����������������ȫ����
  
            dots[i].SetActive(false);//�ر�dot
        }
    }

    private Vector2 DotsPosition(float t)//����˳����صĵ���
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2
            (AimDirection().normalized.x * launchForce.x,
             AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);
        //t�ǿ���֮������
        return position;//����λ��
    }//���õ��ຯ��

}
