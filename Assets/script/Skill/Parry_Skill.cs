using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Perry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked;
    [Header("Restore Health")]
    [SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPerentage;
    public bool restoreUnlocked { get; private set; }
    [Header("Parry Mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }



    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.charactState.GetHealthHP() * restoreHealthPerentage);
            player.charactState.IncreaseHealthBy(restoreAmount);
        }
    }

   public override void Start()
    {
        base.Start();
        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }
    public override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
    }

    private void UnlockParry()
    {
        if (parryUnlockButton.unlock)
        {
            parryUnlocked = true;
        }
    }
    private void UnlockParryRestore()
    {
        if (restoreUnlockButton.unlock)
        {
            restoreUnlocked = true;
        }
    }
    private void UnlockParryWithMirage()
    {
        if (parryWithUnlockButton.unlock)
        {
            parryWithMirageUnlocked = true;
        }
    }

    public void MakeMirageOnParry(Transform _transform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateClonecounterAttack(_transform);
    }
}
