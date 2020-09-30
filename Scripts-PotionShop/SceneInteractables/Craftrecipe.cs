using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftrecipe : MonoBehaviour
{
    public int[] requiredItems;
    public int ItemToCraft;
    public GameObject BluePotion;
    public List<GameObject> Ingredients;
    public GameObject BrewScreen;

    void Start()
    {
        BrewScreen.SetActive(false);
    }


    public Craftrecipe(int ItemToCraft, int[] requiredItems)
    {
        this.requiredItems = requiredItems;
        this.ItemToCraft = ItemToCraft;


    }

    private void Update()
    {
        GameObject ingredients = GameObject.FindGameObjectWithTag("ingredient");

        if (Ingredients.Contains(ingredients) && ingredients != null)
        {
            BrewScreen.SetActive(true);
        }
    }


}
