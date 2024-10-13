using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : MonoBehaviour//��̲�Ӱ
{
    public SpriteRenderer sr;
    private float colorLooseRate;

    public void SetupAfterImage(float _loosingSpeed, Sprite _spriteImage)//���ò�Ӱ
    {
        sr.sprite = _spriteImage;
        colorLooseRate = _loosingSpeed;
    }

    private void Update()
    {
        float alpha = sr.color.a - colorLooseRate * Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
}