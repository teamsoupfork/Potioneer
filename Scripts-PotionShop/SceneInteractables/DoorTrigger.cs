using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    /// <summary>
    /// Creates small signs to show the player that the door has opened as well as the number of customers served via an ipad 
    /// </summary>
    public TextMeshProUGUI TV, iPad;
    public GameObject doorlight, door, 
                        pointLight, TutorialOver;

    Animator door_Animator;
    AudioSource Doorbell;

    int minCustomersServed = 3;
    public static bool served, tutorialOver;

    void Start()
    {
        TV.text = "Open";
        door_Animator = door.GetComponent<Animator>();
        Doorbell = GetComponent<AudioSource>();
        minCustomersServed = PhoneScript.currentAmount;
        iPad.text = minCustomersServed.ToString();
    }

    void Update()
    {
        //checks the counter on the ipad is correct
        if (served)
        {
            minCustomersServed = PhoneScript.currentAmount;
            iPad.text = minCustomersServed.ToString();
            served = false;
        }
    }

    //door opens and flashes a light at customers and player
    private void OnTriggerEnter(Collider other)
    {      
        if(other.gameObject.CompareTag("customer"))
        {
            TV.text = "Welcome";
            doorlight.GetComponent<Renderer>().material.color = Color.green;
            door_Animator.SetBool("Open", true);
            pointLight.SetActive(true);
        }        
        if(other.gameObject.CompareTag("Player"))
        {
            TV.text = "Come back soon!";
            door_Animator.SetBool("Open", true);
            pointLight.SetActive(true);
            tutorialOver = true;
            MandyDialogue.run = true;
            MandyDialogue.tutorial = false;
        }
        Doorbell.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("customer"))
        {
            TV.text = "Open";
            doorlight.GetComponent<Renderer>().material.color = new Color(0,255,231);
            door_Animator.SetBool("Open", false);
            pointLight.SetActive(false);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            TV.text = "Open";
            door_Animator.SetBool("Open", false);
            pointLight.SetActive(false);
        }
    }
}
