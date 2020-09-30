using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MandyDialogue : MonoBehaviour
{
    public TextMeshProUGUI TiptextDisplay;
    public static bool tutorial;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton, dialogueBox;

    public Animator dialogueDisplayAnim;

    public static bool ispaused = false;
    public static bool run;
    public GameObject OptionsMenu;

    void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            TiptextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
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


    void Update()
    {
        if (tutorial && !run)
        {
            ClockUI.paused = true;
            if (TiptextDisplay.text == sentences[index])
            {
                dialogueDisplayAnim.SetBool("IsClose", true);
                continueButton.SetActive(true);
            }
        }
        else
        {
            ClockUI.paused = false;
        }

    }

    public void NextSentence()
    {
        Time.timeScale = 1f;
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            TiptextDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            TiptextDisplay.text = "";
            dialogueDisplayAnim.SetBool("IsClose", false);
            continueButton.SetActive(false);
            OptionsMenu.SetActive(true);
            Time.timeScale = 1f;
        }
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

    public void CatTransform()
    {
        OptionsMenu.SetActive(false);
        TransformAnimal.catBool = true;
    }

    public void PenguinTransform()
    {
        OptionsMenu.SetActive(false);
        TransformAnimal.penguinBool = true;
    }

}
