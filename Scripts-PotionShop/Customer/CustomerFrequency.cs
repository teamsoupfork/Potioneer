using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
/// <summary>
/// Handles the frequency at which customer spawns
/// </summary>
/// <ref>
/// https://answers.unity.com/questions/898380/spawning-an-object-at-a-random-time-c.html Ref for random spawning within a time range
/// </ref>
public class CustomerFrequency : MonoBehaviour
{
    public static int customerCount = 0;

    public float minTime = 5;
    public float maxTime = 10;
    public GameObject tutorialWall, dialogueTutorial, dialogueManager;

    //List of customers that can spawn
    public List<GameObject> customerList;

    private float spawnTime; // Time to spawn object

    public Transform spawnPoint;

    public static int maxCustomerCount = 1;

    public float time;
    int CustomerRandom;

    public bool tutorialcustomer;
    public static bool tutorialdone;
    void Start()
    {
        customerCount = 0;
        if (!tutorialdone)
        {
            tutorialcustomer = false;
        }
        if (DoorTrigger.tutorialOver)
        {
            dialogueManager.SetActive(false);
            dialogueTutorial.SetActive(false);          
        }
    }

    void Update()
    {
        CustomerRandom = Random.Range(0, 2);
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0 && !ClockUI.paused && tutorialdone)
        {          
            time = 0;
            if (customerCount < maxCustomerCount)
            {
                StartCoroutine(SpawnCustomer());
            }
            else if (customerCount == maxCustomerCount || ClockUI.paused)
            {
                StopCoroutine(SpawnCustomer());
            }
        }

        if (tutorialdone)
        {
            tutorialWall.SetActive(false);
        }

        if (!tutorialcustomer)
        {
            StartCoroutine(SpawnTutorialCustomer());
        }
        else if (tutorialcustomer)
        {
            StopCoroutine(SpawnTutorialCustomer());
        }
    }

    //Spawns the customer after a random amount of time.
    IEnumerator SpawnCustomer()
    {
        for(int i = customerCount; i < maxCustomerCount; i++)
        {
            customerCount++;
            Customerspawning();
            spawnTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    IEnumerator SpawnTutorialCustomer()
    {
        for (int i = customerCount; i < maxCustomerCount; i++)
        {
            customerCount++;
            tutorialcustomer = true;
            Instantiate(customerList[1], spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(5);
        }
    }
        
    //Spawns a random Customer from a list 
    void Customerspawning()
    {
        Instantiate(customerList[CustomerRandom], spawnPoint.position, spawnPoint.rotation);       
    }

    public List<GameObject> getCustomerList()
    {
        return customerList;
    }  
}