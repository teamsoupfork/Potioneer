using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject playerGrab,newObj, InteButton, Player;
    public List<GameObject> Ingredients;
    public GameObject BlueBrewScreen;
    public GameObject RedBrewScreen;

    static bool handFull;


    void Start()
    {
        playerGrab = GameObject.Find("Grab");
        InteButton = GameObject.Find("IntCan");
        Player = GameObject.FindGameObjectWithTag("player");
        BlueBrewScreen = GameObject.Find("Brewing Confirmation Blue");
        RedBrewScreen = GameObject.Find("Brewing Confirmation Red");
    }
    //controls the interactions with cauldron
    public void OninteractCauldron()
    {

        GameObject Basil = GameObject.Find("BASIL");
        GameObject Sage = GameObject.Find("SAGE");
        if (handFull == true)
        {
            Ingredients.Add(Basil);
            Destroy(Basil);
            Debug.Log(Basil.name + " burned in the cauldron");
            handFull = false;
            
        }
        else if (handFull)
        {
            Ingredients.Add(Sage);
            Destroy(newObj);
            Debug.Log(newObj.name + " burned in the cauldron");
            handFull = false;
        }



        if (Ingredients.Contains(Basil))
        {
            BlueBrewScreen.SetActive(true);
        }
        else if(Ingredients != null && Ingredients.Contains(Sage))
        {
            RedBrewScreen.SetActive(true);
        }

    }
}
