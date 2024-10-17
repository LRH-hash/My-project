using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] Slider slider;
    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskholeImage;

    [SerializeField] private TextMeshProUGUI currentSouls;
    public float SoulsAmount;
    public float SoulIncrease = 100;

    private SkillManager skills;
    void Start()
    {
        if (playerStats != null)
        {
            playerStats.UpHealth += UpdateHealthUI;
        }

        skills = SkillManager.instance;


    }

    // Update is called once per frame
    void Update()
    {
        if (SoulsAmount < PlayerManager.instance.GetCurrentAmount())
        {
            SoulsAmount += Time.deltaTime * SoulIncrease;
        }
        else
            SoulsAmount = PlayerManager.instance.GetCurrentAmount();
        currentSouls.text =((int)SoulsAmount).ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift))//使用技能后图标变黑
        {
            SetCoolDownOf(dashImage);
        }
        if (Input.GetKeyDown(KeyCode.Q) && skills.parry.parryUnlocked)
        {
            SetCoolDownOf(parryImage);
        }
        if (Input.GetKeyDown(KeyCode.F) )
        {
            SetCoolDownOf(crystalImage);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetCoolDownOf(swordImage);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetCoolDownOf(blackholeImage);
        }
        if (Input.GetKeyDown(KeyCode.E) && Inventory.Instance.GetEquipment(equirmentType.Flask) != null)
        {
            SetCoolDownOf(flaskholeImage);
        }

        CheckCooldown(dashImage, skills.dash.cooldowntime);
        CheckCooldown(parryImage, skills.parry.cooldowntime);
        CheckCooldown(crystalImage, skills.cyrstal.cooldowntime);
        CheckCooldown(swordImage, skills.Sword.cooldowntime);
        CheckCooldown(blackholeImage, skills.blackHole.cooldowntime);
        if (Inventory.Instance.GetEquipment(equirmentType.Flask) != null)
        {
            CheckCooldown(flaskholeImage, Inventory.Instance.GetEquipment(equirmentType.Flask).cooldown);
        }
    }

    private void UpdateHealthUI()//更新血量条函数，此函数由Event触发
    {
        slider.maxValue = playerStats.GetHealthHP();
        slider.value = playerStats.currentHP;
    }

    private void SetCoolDownOf(Image _image)//使用技能后使图标变黑的函数
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldown(Image _image, float _cooldown)//使图标根据cd逐渐变白的函数
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}
