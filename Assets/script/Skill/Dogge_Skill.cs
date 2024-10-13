using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Dogge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockDoggeButton;
    [SerializeField] private int evasionAmount;
    public bool doggeUnlocked;

    [Header("Mirage dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockMirageDoggeButton;
    public bool dodgemirageUnlocked;

    public override void Start()
    {
        base.Start();

        unlockDoggeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockMirageDoggeButton.GetComponent<Button>().onClick.AddListener(UnlockMirageDogge);
    }
    private void UnlockDodge()
    {
        if (unlockDoggeButton.unlock)
        {
            player.charactState.evasion.AddModify(evasionAmount);
            Inventory.Instance.UpdateStatUI();
            doggeUnlocked = true;
        }
    }
    public override void CheckUnlock()
    {
        UnlockDodge();
        UnlockMirageDogge();
    }
    private void UnlockMirageDogge()
    {
        if (unlockMirageDoggeButton.unlock)
            dodgemirageUnlocked = true;
    }
    public void CreateMirageOnDoDogge()
    {
        if (dodgemirageUnlocked)
        {
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
