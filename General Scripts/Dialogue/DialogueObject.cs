using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueObject : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject ObjectDialogue;

    public Animator dialogueDisplayAnim;

    public static bool ispaused = false;

    public void OnMouseDown()
    {
        ObjectDialogue.SetActive(true);
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {

            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(Type());
        ObjectDialogue.SetActive(false);
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
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

    }

    public void NextSentence()
    {
        Time.timeScale = 1f;
        continueButton.SetActive(false);

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
            continueButton.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void PauseGame()
    {
        ObjectDialogue.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
        Debug.Log("Paused");
    }


    void Resumegame()
    {
        ObjectDialogue.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
        Debug.Log("Resumed");
    }
}
