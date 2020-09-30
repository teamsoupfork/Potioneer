using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    Animator SnoozeAnim;
    public GameObject pauseMenuUI;
    bool press;
    public LevelChanger Transitions;

    public void GoToShopScene()
    {
        SceneManager.LoadScene("ShopScene");
        pauseMenuUI.SetActive(false);
    }

    public void ToDelegate()
    {
        //sets animator to press button down
        if (this.name == "Skip")
        {
            press = true;
            if (press)
            {
                SnoozeAnim = this.GetComponent<Animator>();
                SnoozeAnim.Play("Pressed");
            }
            //delays time as player sleeps in
            PhoneScript.delayed = true;
            TimeManager.ShopTime = 0.25f;
            TimeManager.ClockTime = 0f;
            StartCoroutine(LeaveHome(3));
        }
        else if (this.name == "Continue")
        {
            TimeManager.ClockTime = 0f;
            TimeManager.ShopTime = 0f;
            WayPointController.morning = true;
            StartCoroutine(LeaveHome(4));
        }     
    }

    //wait before transitioning to next scene
    IEnumerator LeaveHome(int i)
    {
        TimeManager.firstRun = true;
        yield return new WaitForSeconds(i);
        Transitions.FadeToDelegate();
    }

    


}
