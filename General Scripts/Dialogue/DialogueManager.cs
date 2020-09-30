using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject DialogPanel;
    public Animator dialogueDisplayAnim;
    public float typingSpeed;

    //Reads from the dialogs txt document
    string filename = "dialogs.txt";
    string[] lines = new string [3];

    private int index;

    Dictionary<HerbType, string> dialogs = new Dictionary<HerbType, string>();

    IEnumerator Type()
    {
        foreach (char letter in filename.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Start()
    {
        StartCoroutine(Type());
        DialogPanel.SetActive(false);
    }

    public void Awake()
    {
        HerbTrigger[] triggers = (HerbTrigger[])FindObjectsOfType(typeof(HerbTrigger));

        foreach (HerbTrigger trigger in triggers)
        {
            trigger.NotifyHerbSelected += OnHerbSelected;
            trigger.NotifyHerbDone += OnHerbDone;
        }
        //Reads dialog from the txt document
        ReadText();
        //Makes the corresponding dialog sentences appear in the dialog box according to the NPC type
        InitDialogs();
    }

    //Herb Type to the dialog that will appear accordingly 
    void InitDialogs()
    {
        dialogs.Add(HerbType.BASIL, lines[0]);
        dialogs.Add(HerbType.SAGE, lines[1]);
        dialogs.Add(HerbType.MANDRAKE, lines[2]);
    }


    private void OnHerbSelected(HerbType herbType, bool done)
    {
        //After the dialog appears, it won't appear again in the scene played the second time
        if (done == false)
        {
        DialogPanel.SetActive(true);
            //Display dialog according to the type of herb being triggered
        textDisplay.text = dialogs[herbType];
        }

    }

    private void OnHerbDone()
    {
        DialogPanel.SetActive(false);
        textDisplay.text = "";
    }


    void Update()
    {

    }

    //Read the Text from the dialogs.txt document.
    void ReadText()
    {
        var source = new StreamReader(Application.dataPath + "/" + filename);
        var fileContents = source.ReadToEnd();
        source.Close();
        lines = fileContents.Split("\n"[0]);
        //foreach (string line in lines)
        //{
        //    Debug.Log(line);
        //}
    }
}
