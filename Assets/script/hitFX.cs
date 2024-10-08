using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitFX : MonoBehaviour
{
    public Material hit;
    public float hitFXtime = 0.2f;
    private Material OranalMaterial;
    public SpriteRenderer sc;
    public Color[] RedColor;
    public Color[] BlueColor;
    public Color[] YellowColor;

    // Start is called before the first frame update
    void Start()
    {

        sc = GetComponent<SpriteRenderer>();
        OranalMaterial = sc.material;
    }

    // Update is called once per frame

    IEnumerator FX()
    {

        sc.material = hit;
        Color currentColor = sc.color;
        sc.color = Color.white;
        yield return new WaitForSeconds(hitFXtime);
        sc.color = currentColor;
        sc.material = OranalMaterial;
    }
    public void RedBlink()
    {
        if (sc.color != Color.white)
            sc.color = Color.white;
        else
            sc.color = Color.red;
    }
    public void whiltBlink()
    {
        CancelInvoke();
        sc.color = Color.white;
    }
    public void IsignitedBlink()
    {
        if (sc.color != RedColor[0])
            sc.color = RedColor[0];
        else
            sc.color = RedColor[1];
    }
    public void IschillBlink()
    {
        if (sc.color !=BlueColor[0])
            sc.color =BlueColor[0];
        else
            sc.color = BlueColor[1];
    }

    public void IsshockBlink()
    {
        if (sc.color != YellowColor[0])
            sc.color = YellowColor[0];
        else
            sc.color = YellowColor[1];
    }
    public void IgniteFor(float _secondTime)
    {
        InvokeRepeating("IsignitedBlink", 0, .3f);
        Invoke("whiltBlink", _secondTime);
    }
    public void ChillFor(float _secondTIme)
    {
        InvokeRepeating("IschillBlink", 0, .3f);
        Invoke("whiltBlink", _secondTIme);
    }
    public void ShockFor(float _secondTime)
    {
        InvokeRepeating("IsshockBlink", 0, .3f);
        Invoke("whiltBlink", _secondTime);
    }
}



