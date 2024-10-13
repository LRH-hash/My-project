using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class hitFX : MonoBehaviour
{
    public GameObject textprefab;
    [Header("image fx")]
    public GameObject afterimagePrefab;
    public float colorlooseRote;
    public float afterimageCoolDown;
    public float afterimageCoolTimer;
    [Header("Screen Shake")]
    public CinemachineImpulseSource Screen;
    public Vector2 shakeSword;
    public Vector2 shakeDamage;
    public float shakeMultiplier;
    public Material hit;
    public float hitFXtime = 0.2f;
    private Material OranalMaterial;
    public SpriteRenderer sc;
    public Color[] RedColor;
    public Color[] BlueColor;
    public Color[] YellowColor;
    public ParticleSystem ignited;
    public ParticleSystem chilled;
    public ParticleSystem shocked;
    public GameObject attackFX;
    public GameObject criticalFX;
    public Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = Color.white;
        Screen = GetComponent<CinemachineImpulseSource>();
        sc = GetComponent<SpriteRenderer>();
        OranalMaterial = sc.material;
    }
    private void Update()
    {
        afterimageCoolTimer -= Time.deltaTime;
    }
    public void creatText(string _text,Color?color=null)
    {
        if (color == null)
            color = defaultColor;
        float x = Random.Range(-1, 1);
        Vector3 offset = new Vector3(x,0,0);
        GameObject newgameobject = Instantiate(textprefab,transform.position+offset, Quaternion.identity);
        newgameobject.GetComponent<TextMeshPro>().text = _text;
        newgameobject.GetComponent<TextMeshPro>().color =(Color)color;
    }
    public void ScreemShake(Vector2 _shakePower)
    {
        Screen.m_DefaultVelocity=new Vector2(PlayerManager.instance.player.moveRight*_shakePower.x,_shakePower.y)*shakeMultiplier;
        Screen.GenerateImpulse();
    }
    // Update is called once per frame
    public void CreateAfterImage()
    {
        if (afterimageCoolTimer < 0)
        {
            afterimageCoolTimer = afterimageCoolDown;
            GameObject newobject = Instantiate(afterimagePrefab, transform.position, transform.rotation);
            newobject.GetComponent<AfterImageFX>().SetupAfterImage(colorlooseRote, sc.sprite);
        }
    }
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
        ignited.Stop();
        chilled.Stop();
        shocked.Stop();
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
        ignited.Play();
        InvokeRepeating("IsignitedBlink", 0, .3f);
        Invoke("whiltBlink", _secondTime);
    }
    public void ChillFor(float _secondTIme)
    {
        chilled.Play();
        InvokeRepeating("IschillBlink", 0, .3f);
        Invoke("whiltBlink", _secondTIme);
    }
    public void ShockFor(float _secondTime)
    {
        shocked.Play();
        InvokeRepeating("IsshockBlink", 0, .3f);
        Invoke("whiltBlink", _secondTime);
    }
    public void CreateHitFx(Transform _target,bool critical)
    {
        GameObject prefab = attackFX;
        if (critical)
            prefab = criticalFX;
        float zRotation = Random.Range(-90, 90);;
        GameObject newgameObject = Instantiate(prefab, _target.position, Quaternion.identity);
        if (critical == true)
        {
            newgameObject.transform.localScale = new Vector3(_target.GetComponent<entity>().knockBackDir, 1, 1);
        }
        newgameObject.transform.Rotate(new Vector3(0, 0, zRotation));


        Destroy(newgameObject, .5f);
    }
}



