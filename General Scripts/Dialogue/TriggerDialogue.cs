using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject ObjectDialogue;

    public Animator dialogueDisplayAnim;


    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //Dialog appears When player enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectDialogue.SetActive(true);
        }
    }

    void Start()
    {
        StartCoroutine(Type());
        ObjectDialogue.SetActive(false);
    }

    //The continuation of the dialog will only continue when the finishes its previous sentence
    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        //dialogueDisplayAnim.SetTrigger("Change");
    }

    //If the sentences in the dialog is not complete it will display the next sentence until all dialog index is complete before closing.
    public void NextSentence()
    {

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
        }
    }

}
