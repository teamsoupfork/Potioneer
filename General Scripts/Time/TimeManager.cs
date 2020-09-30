using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Main Scoring System of the game
    /// </summary>
    public static float TotalTime,  Multiplier, ClockTime, 
                        TempTime, ShopTime, ShopValue, customersServed, initCustomerCount,
                        currBal, Profit, Gain, TotalScore;
    public static int extraTime;
    public static bool StartPzzl, Cont, firstRun, EndofDay;

    public static string title;
    public int ClockLimiter = 30; //Can be modified to give more time to solve puzzle in Delegation. Default is 30 seconds.
    public GameObject EndMenu;
    void Awake()
    {
        TotalTime = 0;

        //Slider Puzzle Multiplier
        Multiplier = 1f;

        //Time given as leeway for player to collect ingredients/deliver packages
        TempTime = 0f;
    }

    private void Start()
    {
        //Giving the player their starting amount of money
            currBal = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cont)
        {
            ChangeToSchedule();
        }
        //ST.text = "Time Left : " + ScheduleTime.ToString();
        ToProceed();
        //if time is up, the day will end
        if (EndofDay)
        {
            title = "Day Failed";
            End();
            DayEnd(title, TotalTime, customersServed, Profit, TotalScore);
            Instantiate(EndMenu);
            EndofDay = false;
        }
        //if number of customers served is 0, the day will end
        if(PhoneScript.currentAmount <= 0)
        {
            End();
            title = "Day Successful";
            Time.timeScale = 0f;
            DayEnd(title, TotalTime, customersServed, Profit, TotalScore);
            Instantiate(EndMenu);
            PhoneScript.currentAmount = 1;
        }
    }

    //Gain a multiplier for taking time to solve the slider puzzle
    void ChangeToSchedule()
    {
        //less time taken to solve the puzzle the higher the multiplier
        if(ClockTime <= ClockLimiter && ClockTime != 0f)
        {
            Multiplier = 5f;
        }
        //for even attempting to do the puzzle and solving it outside the set time a smaller multiplier is given
        else if(ClockTime > ClockLimiter)
        {
            Multiplier = 3f;
        }
    }
    void ToProceed() //For calculation of Multiplier in SliderHint
    {
        if (StartPzzl) //found in Puzzle.cs
        {
            ClockTime += Time.deltaTime; //time runs while player attempts to solve the puzzle
        }
    }

    //caluculates the final values of the scoring
    void End()
    {
        TotalTime = Multiplier * (customersServed / Mathf.Round(ShopTime));
        Profit = Gain + currBal;
        TotalScore = Mathf.Round(Profit / TotalTime * 60);
    }

    //sets the values in place when end screen appears
    public void DayEnd(string Title, float totalTime, float custServed, float Profit, float totalScore)
    {
        EndMenu.transform.GetChild(3).GetComponent<Text>().text = Title;
        EndMenu.transform.GetChild(4).GetComponent<Text>().text = totalTime.ToString();
        EndMenu.transform.GetChild(6).GetComponent<Text>().text = "$" + Profit.ToString();
        EndMenu.transform.GetChild(5).GetComponent<Text>().text = custServed.ToString();
        EndMenu.transform.GetChild(7).GetComponent<Text>().text = "$" + totalScore.ToString() + "per minute";       
    }
}
