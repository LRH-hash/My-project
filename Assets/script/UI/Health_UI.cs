using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public entity entity;
    public RectTransform Recttransform;
    public Slider slider;
    public CharactState stat;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        entity = GetComponentInParent<entity>();
        Recttransform = GetComponent<RectTransform>();
        stat = GetComponentInParent<CharactState>();
        entity.OnFlip += FlipUI;
        stat.UpHealth += UpdataHealthUI;
        UpdataHealthUI();
    }

    // Update is called once per frame
    void Update()
    
    {
    }
    public void FlipUI()
    {
        Recttransform.Rotate(0, 180, 0);
    }
    public void UpdataHealthUI()
    {
        slider.maxValue = stat.GetHealthHP();
        slider.value = stat.currentHP;
    }
    public void OnDisable()
    {
        entity.OnFlip -= FlipUI;
        stat.UpHealth -= UpdataHealthUI;
    }
}
