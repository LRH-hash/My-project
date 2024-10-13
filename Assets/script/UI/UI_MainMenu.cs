using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    public GameObject continueButthon;
    public UI_FadeScene fadeScene;
    private void Start()
    {
        if (SaveManager.instance.HasSaveData())
            continueButthon.SetActive(true);
        else
            continueButthon.SetActive(false);
        fadeScene = GetComponentInChildren<UI_FadeScene>();
    }
    public void EnterMainScene()
    {
        StartCoroutine("FadeScene");
    }
    public void NewGame()
    {
        SaveManager.instance.Delete_File();
        SceneManager.LoadScene("MainScene");
    }
    public void ExitGame()
    {
        
    }
    public IEnumerator FadeScene()
    {
        fadeScene.FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainScene");
    }
}
