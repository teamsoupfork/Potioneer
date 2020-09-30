using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

using System.IO;

public class NPCDialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject DialogPanel;
    //public Animator dialogueDisplayAnim;
    public float typingSpeed;

    //Reads from the NPCdialog txt document
    string filename = "NPCdialog.txt";
    //The amount of data from the txt document being extracted
    string[] line = new string [10];

    private int index;

    Dictionary<NPCType, string> NPCdialog = new Dictionary<NPCType, string>();

    // Start is called before the first frame update

    void Start()
    {
        DialogPanel.SetActive(false);
    }

    public void Awake()
    {
        NPC[] triggers = (NPC[])FindObjectsOfType(typeof(NPC));
        foreach (NPC trigger in triggers)
        {
            trigger.NotifyNpcSelected += OnNpcSelected;
            trigger.NotifyNpcDone += OnNpcDone;
        }
        //Reads dialog from the txt document
        ReadText();
        //Makes the corresponding dialog sentences appear in the dialog box according to the NPC type
        InitDialog();
    }


    //NPC Type to the dialog that will appear accordingly 
    void InitDialog()
    {
        NPCdialog.Add(NPCType.MKTNPC, line[0]);
        NPCdialog.Add(NPCType.RESBLK1NPC, line[1]);
        NPCdialog.Add(NPCType.RESBLK1NPC2, line[2]);
        NPCdialog.Add(NPCType.RESBLK2NPC, line[3]);
        NPCdialog.Add(NPCType.RESBLK3NPC, line[4]);
        NPCdialog.Add(NPCType.PRIRESNPC, line[5]);
        NPCdialog.Add(NPCType.PRIRESNPC2, line[6]);
        NPCdialog.Add(NPCType.FARMNPC, line[7]);
        NPCdialog.Add(NPCType.CHILDNPC, line[8]);
        NPCdialog.Add(NPCType.CHILDNPC2, line[9]);
    }


    private void OnNpcSelected(NPCType npcType)
    {
        DialogPanel.SetActive(true);
        //Display dialog according to the type of NPCs being triggered
        textDisplay.text = NPCdialog[npcType];
    }

    private void OnNpcDone()
    {
        DialogPanel.SetActive(false);
        textDisplay.text = "";
    }


    void Update()
    {

    }

    //Read the text from the NPCdialog.txt document
    void ReadText()
    {
        var source = new StreamReader(Application.dataPath + "/" + filename);
        var fileContents = source.ReadToEnd();
        source.Close();
        line = fileContents.Split("\n"[0]);
        //foreach (string line in line)
        //{
        //    Debug.Log(line);
        //}
    }
}
