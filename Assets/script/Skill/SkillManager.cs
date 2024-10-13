using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Skill_dash dash { get; private set; } 
    public Skill_Clone clone { get; private set; }
    public Sword_Skill Sword { get; private set; }
    public Skill_BlackHole blackHole { get; private set; }
    public Cyrstal_Skill cyrstal { get; private set; }
    public Parry_Skill parry { get; private set; }
    public Dogge_Skill Dogge { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        dash = GetComponent<Skill_dash>();
        clone = GetComponent<Skill_Clone>();
        Sword = GetComponent<Sword_Skill>();
        blackHole = GetComponent<Skill_BlackHole>();
        cyrstal = GetComponent<Cyrstal_Skill>();
        parry = GetComponent<Parry_Skill>();
        Dogge = GetComponent<Dogge_Skill>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
