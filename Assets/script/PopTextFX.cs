using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopTextFX : MonoBehaviour
{
    public TextMeshPro mytext;
    public float speed;
    public float disappearancespeed;
    public float colorddisappearancespeed;
    public float lifttime;
    private float duringtimer;
    private void Start()
    {
        duringtimer = lifttime;
    }
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        duringtimer -= Time.deltaTime;
        if(duringtimer<0)
        {
            float apaha = mytext.color.a - colorddisappearancespeed * Time.deltaTime;
            mytext.color = new Color(mytext.color.r, mytext.color.g, mytext.color.b, apaha);
            if (mytext.color.a < 50)
                speed = disappearancespeed;
            if (mytext.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
