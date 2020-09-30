using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerPath : MonoBehaviour
{
    /// <summary>
    /// Manages the events and actions that occur when the customer is at different locations
    /// </summary>
    GameObject[] points;
    public Collider CustCol;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public GameObject customer;

    Transform target;
    public int customerCount;
    public bool customerWait;
    public bool occupied;
    public bool shiftStart, isDone;
    GameObject particlePoint; //point where particles will spawn based on customer mood

    public float InternalTime;
    public ParticleSystem trail;
    public GameObject CustomerNeeds;

    //controls what particle spawns based on whether the customer is happy or angry when potion is served or not served at all.
    public GameObject angry, tired, happy;
    public static bool bad, okay, good;
    public int PsCount;
    ParticleSystem Ps;


    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Waypoint");
        particlePoint = GameObject.Find("Anger");
        CustCol = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;

        CustomerNeeds.SetActive(false);
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        target = GameObject.Find("Player").transform;
        agent.autoBraking = false;


        //Order = GetComponent<Button>();
        InternalTime = 50f;

        if (trail.isPlaying)
        {
            trail.Stop();
        }

    }

    IEnumerator OnTheMove()
    {
        if (!customerWait)
        {
            GotoNextPoint();
            while (customerWait)
            {
                yield return null;
            }
            yield return null;
        }
    }
    //controls the navmesh movement of the object.
    void GotoNextPoint()
    {
        //if there are no waypoints set
        if (points.Length == 0)
        {
            return;
        }
        if (Vector3.Distance(points[destPoint].transform.position, customer.transform.position) < 3)
        {
            destPoint++;

            if (destPoint >= points.Length) //destroys object at last waypoint.
            {
                Destroy(gameObject);
                CustomerFrequency.customerCount--;
            }
            if (destPoint < points.Length)
            {
                //set destination of the waypoint
                agent.SetDestination(points[destPoint].transform.position);
            }     
        }
       
        if (!trail.isPlaying)
        {
            trail.Play();
        }
    }

    void Update()
    {
        if (shiftStart)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                StartCoroutine(OnTheMove());
            }
            SpawnParticleSystem();
            CustomerWaitingTime();
        }

        if (CustomerInteraction.received /*&& CustomerInteraction.paid*/)
        {
            if (!isDone)
            {
                isDone = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "WantCollider")
        {
            CustomerNeeds.SetActive(true);
            trail.Stop();
            if (!MandyDialogue.tutorial)
            {
               
                customerWait = true;
            }
        }
        if (other.gameObject.CompareTag("customer")) //allows for other customers to queue behind the customer at the table
        {
            Rigidbody rb = agent.GetComponent<Rigidbody>();
            trail.Stop();
            SlowRotate();
            agent.isStopped = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }

    //makes the object stay within the collider as well as starting a wait timer via a bool.
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "WantCollider")
        {
            Rigidbody rb = agent.GetComponent<Rigidbody>();
            if (target != null)
            {
                SlowRotate();
                agent.isStopped = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }
            if (isDone)
            {
                
                rb.constraints = RigidbodyConstraints.FreezeRotationY;
                agent.isStopped = false;
                customerWait = false;
            }
        }

    }
    //On exit the customer will stop queuing and start moving towards the cash register again.
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = agent.GetComponent<Rigidbody>();
        if (other.gameObject.CompareTag("customer"))
        {
            Debug.Log("not queueing");
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            agent.isStopped = false;
            CustomerInteraction.redBool = false;
            CustomerInteraction.blueBool = false;
            CustomerInteraction.received = false;
            CustomerInteraction.paid = false;
            CustomerNeeds.SetActive(false);
        }
    }
    void SlowRotate()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position); //finds the quanterion that allows the customer to face the player position
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); //changes the current rotation to set rotation in a specific amount of time
    }


    void CustomerWaitingTime()
    {
        if (customerWait)
        {
           // CustomerNeeds.SetActive(true);
            if (InternalTime < 30)
            {
                okay = true;
            }
            else if (InternalTime < 15)
            {
                bad = true;
                okay = false;
            }
            if (InternalTime >= 0)
            {
                InternalTime -= Time.deltaTime;
            }
            else if (InternalTime < 0)
            {
                CustomerNeeds.SetActive(false);
                bad = true;
                isDone = true;
                Destroy(Ps);
                PsCount = 0;
                InternalTime = 0;
                customerWait = false;
            }
        }
    }
    void SpawnParticleSystem()
    {
        if (bad && PsCount < 1 )
        {
            PsCount++;
            Ps = Instantiate(angry, particlePoint.transform.position, particlePoint.transform.rotation).GetComponent<ParticleSystem>();
            Ps.transform.parent = particlePoint.transform;
            Ps.Play();
            bad = false;
            Destroy(Ps.gameObject, 1);
        }
        else if (okay && PsCount < 1 )
        {
            Ps = Instantiate(tired, particlePoint.transform.position, particlePoint.transform.rotation).GetComponent<ParticleSystem>();
            Ps.transform.parent = particlePoint.transform;
            Ps.Play();
            PsCount++;
            okay = false;
            Destroy(Ps.gameObject, 2);
        }
        else if (good && PsCount < 1 )
        {
            Ps = Instantiate(happy, particlePoint.transform.position, particlePoint.transform.rotation).GetComponent<ParticleSystem>();
            Ps.transform.parent = particlePoint.transform;
            PsCount++;
            PhoneScript.currentAmount--;
            TimeManager.customersServed++;
            DoorTrigger.served = true;
            Ps.Play();
            good = false;
            customerWait = false;
            Destroy(Ps.gameObject, 2);
        }
    }
}

