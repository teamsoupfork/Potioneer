using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionStorage : MonoBehaviour
{
    public List<GameObject> PotionsList;
    public GameObject BluePotion;
    public GameObject RedPotion;
    public List<GameObject> spawnPointList;
    public List<GameObject> potionList;
    public Text BlueNo, RedNo;
    private bool part1, part2;

 

    public static int NumberOfBluePotions, NumberOfRedPotions;
    public int OldNumberOfBluePotions, OldNumberOfRedPotions;
    void Start()
    {
        OldNumberOfBluePotions = NumberOfBluePotions = 0;
        OldNumberOfRedPotions = NumberOfRedPotions = 0;
        part1 = false;
        part2 = false;
    }

    // Update is called once per frame
    void Update()
    {       
        PotionToShelf();
    }

    void PotionToShelf()
    {
        BlueNo.text = "x " + NumberOfBluePotions;
        RedNo.text = "x " + NumberOfRedPotions;

        if (!part1 && puzzleCheckBlue.Bluesolved == true && DFPlayer.PotionDouble == true)
        {
           

            part1 = true;
            NumberOfBluePotions++;
            NumberOfBluePotions++;
            puzzleCheckBlue.Bluesolved = false;
            PuzzleCheckRed.Redsolved = false;
            BlueNo.text = "x " + NumberOfBluePotions;
            if (NumberOfBluePotions == OldNumberOfBluePotions + 2)
            {

                Instantiate(potionList[0].transform, spawnPointList[0].transform);
                Instantiate(potionList[0].transform, spawnPointList[0].transform);

            }
        }
        else if (!part1 && puzzleCheckBlue.Bluesolved == true)
        {
            
            part1 = true;
            NumberOfBluePotions++;
            puzzleCheckBlue.Bluesolved = false;
             PuzzleCheckRed.Redsolved = false;
            BlueNo.text = "x " + NumberOfBluePotions;
            if (NumberOfBluePotions == OldNumberOfBluePotions + 1)
            {

                Instantiate(potionList[0].transform, spawnPointList[0].transform);

            }
        }
       
        if (!part2 && PuzzleCheckRed.Redsolved == true && DFPlayer.PotionDouble == true)
        {
            Instantiate(potionList[1].transform, spawnPointList[1].transform);
            part2 = true;
            NumberOfRedPotions++;
            NumberOfRedPotions++;
            PuzzleCheckRed.Redsolved = false;
            puzzleCheckBlue.Bluesolved = false;
            RedNo.text = "x " + NumberOfRedPotions;
        }
        else if (!part2 && PuzzleCheckRed.Redsolved == true)
        {
            Instantiate(potionList[1].transform, spawnPointList[1].transform);
            part2 = true;
            NumberOfRedPotions++;
            PuzzleCheckRed.Redsolved = false;
            puzzleCheckBlue.Bluesolved = false;
            RedNo.text = "x " + NumberOfRedPotions;
        }
        else
            return;      
    }
    
}
