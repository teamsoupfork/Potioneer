using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{
    public List<CraftItem>Items = new List<CraftItem>();

    void Awake()
    {
        BuildItemDatabase();
    }

    public CraftItem GetPotionById(int id)
    {
        return Items.Find(item => item.id == id);
    }

    void BuildItemDatabase()
    {
        Items = new List<CraftItem>()
        {
            new CraftItem(1, "Simple Potion", "A simple potion.", new Dictionary<string, int> {{"Strength", 5}}),
            new CraftItem(2, "Complex Potion", "A More complex potion.", new Dictionary<string, int> {{"Strength", 10}}),
            new CraftItem(3, "Special Potion", "A powerful, special potion.", new Dictionary<string, int> {{"Strength", 15}}),
            new CraftItem(4, "Basil Leaf", "A leaf from the basil plant", new Dictionary<string, int>{{"leafness", 10 } }),
            new CraftItem(5, "Sage leaf", "a leaf from the sage plant", new Dictionary<string, int>{{"leafness", 10 } }),
            new CraftItem(6,  "Dragon Scale", "a scale from a dragon" , new Dictionary<string, int>{{"scaleness", 10 } }),

        }; 
    }

}
