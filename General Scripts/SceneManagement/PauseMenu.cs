
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// pauses the game and opens pause menu
/// 
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static bool ispaused = false;

    public GameObject pauseMenuUI;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void pauseControl()
    {
        if (ispaused)
        {
            Resumegame();
           
        }
        else
        {
            PauseGame();          
        }
    }

    //Resumes the game and set Pause UI inactive
    void Resumegame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
        Debug.Log("Resumed");
    }

    //Pauses the game and set the Pause UI active
    void PauseGame() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
        Debug.Log("Paused");
    }

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        Resumegame();
        SceneManager.LoadSceneAsync("StartingScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }

}
