using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheckRed : MonoBehaviour
{
    //https://www.youtube.com/watch?v=TMQrO3Hy_LE&t=137s
    public GameObject RedPotion;
    public static bool Redsolved;
    public GameObject RedPotionSpawnPoint;
    public GameObject RedBrewScreen;
    public ParticleSystem PsCreation;

    public Transform[] pipe_puzzle;


    void Start()
    {
        //this makes sure the puzzle is always set to false in the beginning
        //PsCreation.Stop();
        Redsolved = false;
    }


    void Update()
    {
        //Checks the red puzzle to see if the pieces are in the right arrangement.
        if (pipe_puzzle[0].rotation.z == 0 &&
            pipe_puzzle[1].rotation.z == 0 &&
            pipe_puzzle[3].rotation.z == 0 &&
            pipe_puzzle[4].rotation.z == 0 &&
            pipe_puzzle[5].rotation.z == 0 &&
            pipe_puzzle[6].rotation.z == 0 &&
            pipe_puzzle[7].rotation.z == 0 &&
            pipe_puzzle[8].rotation.z == 0)
        {
            Redsolved = true;
            RedPotionSpawnPoint.SetActive(true);
            RedBrewScreen.SetActive(false);

        }
        else
        {
            //ensures the puzzle is not permanently set to true.
            Redsolved = false;

        }
    }

}
