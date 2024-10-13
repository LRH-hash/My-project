using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] sfx;
    public AudioSource[] bgm;
    public bool PlayBgM;
    public int bgmindex;
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if(!PlayBgM)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgm[bgmindex].isPlaying)
                bgm[bgmindex].Play();
        }
    }
    public void RandomBgm()
    {
        bgmindex = Random.Range(0, bgm.Length);
        PlayBGM(bgmindex);
    }
    public void PlaySFX(int _sfxindex)
    {
        sfx[_sfxindex].pitch = Random.Range(0.85f, 1.1f);
            sfx[_sfxindex].Play();
    }
    public void StopSFX(int _sfxindex)
    {
        if (_sfxindex < sfx.Length)
            sfx[_sfxindex].Stop();
    }
    public void PlayBGM(int _BGMindex)
    {
        bgmindex = _BGMindex;
        StopAllBGM();
        bgm[_BGMindex].Play();
    }
    public void StopAllBGM()
    {
        for(int i=0;i<bgm.Length;i++)
        {
            bgm[i].Stop();
        }
    }
}
