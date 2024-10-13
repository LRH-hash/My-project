using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeScene : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
        
}
