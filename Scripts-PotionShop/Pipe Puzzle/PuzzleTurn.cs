using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTurn : MonoBehaviour
{
    //https://www.youtube.com/watch?v=TMQrO3Hy_LE&t=137s
    public GameObject BrewPuzzle;
    public GameObject PuzzlePiece1;
    public GameObject PuzzlePiece2;
    public GameObject PuzzlePiece3;
    public GameObject PuzzlePiece4;
    public GameObject PuzzlePiece5;
    public GameObject PuzzlePiece6;
    public GameObject PuzzlePiece7;
    public GameObject PuzzlePiece8;
    public GameObject PuzzlePiece9;


    public void Start()
    {
     

    }
    private void Update()
    {

    }
    public void Rotate()
    {
        if (!puzzleCheckBlue.Bluesolved)
        {
            this.transform.Rotate(0f, 0f, -90f, Space.Self);
        }
        else if (puzzleCheckBlue.Bluesolved)
        {
            BrewPuzzle.SetActive(false);
            puzzleCheckBlue.Bluesolved = false;
            PuzzlePiece1.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece2.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece3.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece4.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece5.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece6.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece7.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece8.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece9.transform.Rotate(0f, 0f, 270f);

        }
        else if (PuzzleCheckRed.Redsolved)
        {
            BrewPuzzle.SetActive(false);
            PuzzleCheckRed.Redsolved = false;
            PuzzlePiece1.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece2.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece3.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece4.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece5.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece6.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece7.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece8.transform.Rotate(0f, 0f, 270f);
            PuzzlePiece9.transform.Rotate(0f, 0f, 270f);

        }
    }
}
