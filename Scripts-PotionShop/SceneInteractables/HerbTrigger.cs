using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Type of herbs.
public enum HerbType { BASIL, SAGE, MANDRAKE }

public class HerbTrigger : MonoBehaviour
{
    public HerbType Herb;
    public GameObject BASIL;
    public GameObject SAGE;
    public GameObject MANDRAKE;

    public delegate void HerbSelectedDelegate(HerbType herbType, bool done);
    public HerbSelectedDelegate NotifyHerbSelected;

    public delegate void HerbDoneDelegate();
    public HerbDoneDelegate NotifyHerbDone;

    public bool done = false;

    //Triggers the dialog according to the trigger and when player leaves trigger it will not appear again the second time.
    void OnTriggerEnter(Collider other)
    {
        NotifyHerbSelected(Herb, done);
        if(done == false)
        {
            done = true;
        }
    }

    //When player moves out of trigger range the dialog will not appear for the second time in the scene.
    void OnTriggerExit(Collider other)
    {
        NotifyHerbDone();

    }
}
