using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleCheckBlue : MonoBehaviour
{
    //https://www.youtube.com/watch?v=TMQrO3Hy_LE&t=137s
    public GameObject bluePotion;
    public static bool Bluesolved;
    public GameObject PotionSpawnPoint;
    public GameObject BrewScreen;
    public ParticleSystem PsCreation;

    public Transform[] pipe_puzzle;

   
    void Start()
    {
        //PsCreation.Stop();
        Bluesolved = false;
    }

   
    void Update()
    {
        //Checks the red puzzle to see if the pieces are in the right arrangement.
        if ( pipe_puzzle[0].rotation.z == 0 &&
            pipe_puzzle[1].rotation.z == 0 &&
            pipe_puzzle[3].rotation.z == 0 &&
            pipe_puzzle[4].rotation.z == 0 &&
            pipe_puzzle[5].rotation.z == 0 &&
            pipe_puzzle[6].rotation.z == 0 &&
            pipe_puzzle[7].rotation.z == 0 &&
            pipe_puzzle[8].rotation.z == 0) 
        {
            Bluesolved = true;
            PotionSpawnPoint.SetActive(true);
            BrewScreen.SetActive(false);

        }
        else
        {
            //helps make sure the puzzle doesnt set itself to true permanently
            Bluesolved = false;
        }
    }
}
