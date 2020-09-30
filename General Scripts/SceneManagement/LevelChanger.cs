using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the transition between scenes
/// </summary>
/// <ref>
/// https://www.youtube.com/watch?v=Oadq-IrOazg&t=256s&ab_channel=Brackeys
/// </ref>
// Adapted by Gabriel Lek
public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    private int sceneToLoad;

    //Changes the scene to the buildindex that is one higher than the current scene.
    //(cannot be modified)
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FadeToDelegate()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //Changes the scene to the buildindex that is one lower than the current scene.
    //(cannot be modified)
    public void FadeToPreviousLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void FadeToStartLevel()
    {
        SceneManager.LoadScene("StartingScene");
    }

    //(cannot be modified)
    public void FadeToLevel(int LevelIndex)
    {
        sceneToLoad = LevelIndex;
        anim.SetTrigger("FadeOut"); 
    }

    //(cannot be modified)
    public void OnFadeEnd()   
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
