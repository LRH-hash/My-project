using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] sfx;
    public AudioClip[] bgm;
    public AudioSource Sfx;
    public AudioSource Bgm;
    public bool PlayBgM;
    public int bgmindex;
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
  
    }
    public void RandomBgm()
    {
        bgmindex = Random.Range(0, bgm.Length);
        PlayBGM(bgmindex);
    }
    public void PlaySFX(int _sfxindex)
    {
        Sfx.clip = sfx[_sfxindex];
    }
    public void StopSFX(int _sfxindex)
    {
        Sfx.Stop();
    }
    public void PlayBGM(int _BGMindex)
    {
        Bgm.clip = bgm[_BGMindex];     
    }
    public void StopBGM()
    {
        Bgm.Stop();
    }
}
