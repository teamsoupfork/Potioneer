using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DFPlayer : MonoBehaviour
{
    HerbType herb;
    HerbType herbType;
    public GameObject HerbInteraction;
    public GameObject CauldronInteraction;
    private GameObject playerGrab, newObj, InteButton, Player;
    public GameObject interactableGrabbed;
    public GameObject BasilTrigger;
    public GameObject SageTrigger;
    public GameObject MandrakeTrigger;
    public GameObject BasilHerb;
    public GameObject SageHerb;
    public GameObject MandrakeHerb;
    public List<GameObject> Ingredients;
    public GameObject BlueBrewScreen;
    public GameObject RedBrewScreen;
    public GameObject CauldronTrigger;
    public GameObject empty;
    public Button HerbButton;
    public Button CauldronButton;
    public Sprite Basil;
    public Sprite Sage;
    public Sprite Mandrake;
    public Sprite Cauldron;

    public ParticleSystem Ps;

    static bool handFull;
    static bool BASIL;
    static bool SAGE;
    static bool MANDRAKE;
    public bool BasilGrabbed;
    public bool SageGrabbed;
    public bool MandrakeGrabbed;
    public static bool SageTouched;
    public static bool BasilTouched;
    public static bool MandrakeTouched;
    public static bool PotionDouble;


    GameObject[] herbPrefabs;

    void Start()
    {
        //this is to set all the canvases to false at start
        playerGrab = GameObject.Find("Grab");
        InteButton = GameObject.Find("IntCan");
        Player = GameObject.FindGameObjectWithTag("player");
        BlueBrewScreen.SetActive(false);
        RedBrewScreen.SetActive(false);
        PotionDouble = false;
        HerbInteraction.SetActive(false);
        CauldronInteraction.SetActive(false);
        //the herbPrefabs loads the resources in gameobjects(herbs)
        herbPrefabs = Resources.LoadAll<GameObject>("Prefabs/Herbs");
        //this checks to see if the player actually collided with a herbTrigger
        HerbTrigger[] triggers =
            (HerbTrigger[])FindObjectsOfType(typeof(HerbTrigger));

        foreach (HerbTrigger trigger in triggers)
        {
            trigger.NotifyHerbSelected += GrabHerb;
        }

        if (Ps.isPlaying)
        {
            Ps.Stop();
        }

      

   
    }

    public void OnTriggerEnter(Collider _other)
    {
        //this sets the buttons based on the collider the player enters
        HerbTrigger herbTrigger = _other.GetComponent<HerbTrigger>();
        SageTrigger = GameObject.FindGameObjectWithTag("Sage");
        BasilTrigger = GameObject.FindGameObjectWithTag("Basil");
        
        if (herbTrigger)
        {
            herb = herbTrigger.Herb;
            Debug.Log(string.Format("{0} selected", herb));

            HerbInteraction.SetActive(true);
        }

        if (CauldronTrigger)
        {
            CauldronInteraction.SetActive(true);
            CauldronButton.image.sprite = Cauldron;
        }

        if (SageTouched == true)
        {
            SAGE = true;
            interactableGrabbed = SageHerb;
            HerbButton.image.sprite = Sage;

        }
        else if (BasilTouched == true)
        {
            BASIL = true;
            interactableGrabbed = BasilHerb;
            HerbButton.image.sprite = Basil;
        }
        else if (MandrakeTouched == true)
        {
            MANDRAKE = true;
            interactableGrabbed = MandrakeHerb;
            HerbButton.image.sprite = Mandrake;
        }
    }

    public void GrabHerb(HerbType _selectedHerb)
    {
        herbType = _selectedHerb;
    }



    public void OnTriggerExit(Collider _other)
    {
        //this makes sure the buttons hide themselves when player leaves a collider
        HerbTrigger herbTrigger = _other.GetComponent<HerbTrigger>();
        if (herbTrigger)
        {
            HerbInteraction.SetActive(false);
        }
        else if (CauldronTrigger)
        {
            CauldronInteraction.SetActive(false);
        }

        if (SAGE == true)
        {
            SAGE = false;
            interactableGrabbed = empty;
            HerbInteraction.SetActive(false);
            CauldronInteraction.SetActive(false);
        }
        else if (BASIL == true)
        {
            BASIL = false;
            interactableGrabbed = empty;
            HerbInteraction.SetActive(false);
            CauldronInteraction.SetActive(false);
        }
        else if (MANDRAKE == true)
        {
            MANDRAKE = false;
            interactableGrabbed = empty;
            CauldronInteraction.SetActive(false);
            HerbInteraction.SetActive(false);
        }
    }

    public void OninteractHerb()
    {
        //this instantiates the herb in the player's hand based on the herb trigger
        if (BASIL == true)
        {
            newObj = Instantiate(interactableGrabbed, playerGrab.transform.position, playerGrab.transform.rotation, playerGrab.transform.parent);
            newObj.name = (string.Format("{0}", herb));
            handFull = true;
            BasilGrabbed = true;
            TrailPlayer.BasilCount--;
        }
        else if (SAGE == true)
        {
            newObj = Instantiate(interactableGrabbed, playerGrab.transform.position, playerGrab.transform.rotation, playerGrab.transform.parent);
            newObj.name = (string.Format("{0}", herb));
            handFull = true;
            SageGrabbed = true;
            TrailPlayer.SageCount--;
        }
        else if (MANDRAKE == true)
        {
            newObj = Instantiate(interactableGrabbed, playerGrab.transform.position, playerGrab.transform.rotation, playerGrab.transform.parent);
            newObj.name = (string.Format("{0}", herb));
            handFull = true;
            MandrakeGrabbed = true;
        }
        if (!Ps.isPlaying)
        {
            Ps.Play();
        }

    }


    public void OninteractCauldron()
    {
        //this checks and sets active either the blue or red potion puzzles
        GameObject Basil = GameObject.Find("BASIL");
        GameObject Sage = GameObject.Find("SAGE");
        GameObject Mandrake = GameObject.Find("MANDRAKE");
        puzzleCheckBlue.Bluesolved = false;
        PuzzleCheckRed.Redsolved = false;


        if (handFull == true && BasilGrabbed == true)
        {
            Ingredients.Add(Basil);
            Destroy(Basil);
            handFull = false;
            BASIL = false;

        }
        else if (handFull == true && SageGrabbed == true)
        {
            Ingredients.Add(Sage);
            Destroy(Sage);
            handFull = false;
            SAGE = false;
        }
        else if (handFull == true && MandrakeGrabbed == true)
        {
            Ingredients.Add(Mandrake);
            Destroy(Mandrake);
            handFull = false;
            MANDRAKE = false;
            Debug.Log(Ingredients.Count);
        }


        if (Ingredients.Contains(Basil))
        {
            RedBrewScreen.SetActive(true);
            BlueBrewScreen.SetActive(false);
            Ingredients.Remove(Basil);
        }
        else if (Ingredients.Contains(Sage))
        {
            BlueBrewScreen.SetActive(true);
            RedBrewScreen.SetActive(false);
            Ingredients.Remove(Sage);
        }
        else if (Ingredients.Contains(Mandrake))
        {
            PotionDouble = true;
            Ingredients.Remove(Mandrake);
        }
        if (Ps.isPlaying)
        {
            Ps.Stop();
        }

    }

    public void GrabHerb(HerbType _selectedHerb, bool done)
    {
        herb = _selectedHerb;
    }

}
