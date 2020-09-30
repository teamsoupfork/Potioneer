using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the changes of scenes from the first scene of the game.
/// </summary>
public class StartMenu : MonoBehaviour
{
    public static bool creditsOn = false;

    public GameObject CreditsUI;

    public AudioClip startGame;
    public AudioSource Source;

   // public LevelChanger Transitions;


    void Start()
    {
        Source.clip = startGame;  
    }

    public void StartGame()
    {
        // anim.Play("FadeOut");
        Source.Play();
        StartCoroutine(Wait(2)); //after 2 seconds change scene.
        SceneManager.LoadScene("SliderPuzzle");       
        //Transitions.FadeToNextLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void creditControl()
    {
        if (creditsOn)
        {
            CreditsSceneInActive();
        }
        else
        {
            CreditsSceneActive();
        }
    }


    public void CreditsSceneActive()
    {
       CreditsUI.SetActive(true);
        creditsOn = true;
    }

    public void CreditsSceneInActive()
    {
        CreditsUI.SetActive(false);
        creditsOn = false;
    }

    
    IEnumerator Wait(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
