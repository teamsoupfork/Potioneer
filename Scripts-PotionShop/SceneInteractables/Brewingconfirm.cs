using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brewingconfirm : MonoBehaviour
{

    public GameObject BlueBrewingPuzzle;
    public GameObject BlueBrewScreen;
    public GameObject RedBrewingPuzzle;
    public GameObject RedBrewScreen;
    public Button Yes;
    public Button Continue;

    // Start is called before the first frame update
    void Start()
    {
        BlueBrewingPuzzle.SetActive(false);
 
        RedBrewingPuzzle.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        BlueBrewScreen = GameObject.Find("Brewing Confirmation Blue");
        RedBrewScreen = GameObject.Find("Brewing Confirmation Red");
    }

   public void TaskYesBlue()
    {
        BlueBrewingPuzzle.SetActive(true);
        BlueBrewScreen.SetActive(false);
    }

   public void TaskContinueBlue()
    {
        BlueBrewScreen.SetActive(false); 
    }

   public void TaskYesRed()
    {
        RedBrewingPuzzle.SetActive(true);
        RedBrewScreen.SetActive(false);
    }

   public void TaskContinueRed()
    {
        RedBrewScreen.SetActive(false);
    }
}
