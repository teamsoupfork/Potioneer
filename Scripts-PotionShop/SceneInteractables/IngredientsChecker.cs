using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsChecker : MonoBehaviour
{
    /// <summary>
    /// Checks on the ingredients available in shop and displays them to the player
    /// </summary>
    
    public GameObject basilcollider, sagecollider;
    public TextMeshProUGUI bCount, sCount, 
                            basilStock, sageStock;
    bool checkOnce;

    void Update()
    {
        bCount.text = TrailPlayer.BasilCount.ToString();
        sCount.text = TrailPlayer.SageCount.ToString();

        //One herb for tutorial purposes
        if (MandyDialogue.tutorial)
        {
            if (!checkOnce)
            {
                sagecollider.SetActive(true);
                TrailPlayer.SageCount++;
                TrailPlayer.Colsage = true;
                checkOnce = true;
            }
        }

        //if Herb not collected or out of stock
        if (TrailPlayer.Colbasil == false || TrailPlayer.BasilCount == 0)
        {
            //prevents interaction with HerbCollider
            basilcollider.SetActive(false);
            //text to alert player of lack of herb in shop
            basilStock.text = "Out of Stock";
        }
        else
        {          
            basilStock.text = "In Stock";
        }
       
        if (TrailPlayer.Colsage == false || TrailPlayer.SageCount == 0)
        {
            //prevents interaction with HerbCollider
            sagecollider.SetActive(false);
            //text to alert player of lack of herb in shop
            sageStock.text = "Out of Stock";
        }       
        else 
        {
            sageStock.text = "In Stock";
        }
    }
}
