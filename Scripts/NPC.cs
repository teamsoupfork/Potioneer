using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Different types of NPCs
public enum NPCType { MKTNPC, RESBLK1NPC, RESBLK1NPC2, RESBLK2NPC, RESBLK3NPC, PRIRESNPC, PRIRESNPC2, FARMNPC, CHILDNPC, CHILDNPC2 }

public class NPC : MonoBehaviour
{
    public NPCType Npc;
    public GameObject MKTNPC;
    public GameObject RESBLK1NPC;
    public GameObject RESBLK1NPC2;
    public GameObject RESBLK2NPC;
    public GameObject RESBLK3NPC;
    public GameObject PRIRESNPC;
    public GameObject PRIRESNPC2; 
    public GameObject FARMNPC;
    public GameObject CHILDNPC; 
    public GameObject CHILDNPC2;

    //Differentate by the type of NPC as they all share the same trigger
    public delegate void NpcSelectedDelegate(NPCType npcType);
    public NpcSelectedDelegate NotifyNpcSelected;

    public delegate void NpcDoneDelegate();
    public NpcDoneDelegate NotifyNpcDone;

    //public bool done = false;


    //Trigger accordding to the type of NPC labelled.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotifyNpcSelected(Npc);
        }     
    }

    //When player moves out of trigger the dialog will not appear again.
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotifyNpcDone();
        }
    }
}
