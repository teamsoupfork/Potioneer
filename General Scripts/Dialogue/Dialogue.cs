using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{/// <summary>
/// <ref></ref>
/// </summary>
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject DialogueBox;

    public Animator dialogueDisplayAnim;

    public static bool ispaused = false;
    public static bool firstrun;
    bool finished;

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //The game would freeze at the start of the game to prevent a loss in time and will start when player clicks on screen
    public void pauseControl()
    {
        if (ispaused)
        {
            Resumegame();

        }
        else
        {
            PauseGame();
        }
    }

    void Start()
    {

    }

    //The continuation of the dialog will only continue when the finishes its previous sentence
    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            NextSentence();
        }
        if (Input.GetMouseButton(0))
        {
            typingSpeed = -1;
        }
        if (finished && !firstrun)
        {
            MandyDialogue.tutorial = true;
            firstrun = true;
        }
    }


    //If the sentences in the dialog is not complete it will display the next sentence until all dialog index is complete before closing.
    public void NextSentence()
    {
        continueButton.SetActive(false);
        Time.timeScale = 0f;

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            dialogueDisplayAnim.SetBool("IsClose", true);
            finished = true;
        }
        continueButton.SetActive(false);
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        ispaused = true;
        ClockUI.paused = true;
    }


    void Resumegame()
    {
        Time.timeScale = 1f;
        ispaused = false;
        ClockUI.paused = false;
    }
}
