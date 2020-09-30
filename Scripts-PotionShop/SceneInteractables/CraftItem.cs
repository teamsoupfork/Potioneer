using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    
    public CraftItem(int id, string  title, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.stats = stats;
    }

    public CraftItem(CraftItem item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
        this.stats = item.stats;
    }

   
}
