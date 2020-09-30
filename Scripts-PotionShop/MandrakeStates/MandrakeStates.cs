using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MandrakeStates : MonoBehaviour
{
    public float wanderRange;

    public GameObject mandrake;
    public GameObject idlePos;

    static Animator anim;
    public UnityEngine.AI.NavMeshAgent agent;
    public float time;

    public bool caught; //sets to true when mandrake is caught
    

    //<erf>
    //https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/ //roaming script ref
    //Adapted by Gabriel lek
    //</ref>
    enum Mandrake
    {
        Idle,
        Roam,

    };
    Mandrake state;

    void Start()
    {
        state = Mandrake.Idle;
        anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        time = 10f;
        caught = false;
    }

    //update function would change the states of the mandrake
    public void Update()
    {
        switch (state) 
        {
            case Mandrake.Idle:
                MandrakeIdleTimer();
                MandrakeRoaming();
                break;
            case Mandrake.Roam:
                MandrakeRoaming();
                break;
        }

    }

    //Mandrake will have a timer before it starts roaming around the scene.
    public void MandrakeIdleTimer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            state = Mandrake.Idle;
        }
        else if (time <= 0)
        {
            time = 0;
            state = Mandrake.Roam;
            Debug.Log("mandrake is roaming");
        }

    }

    //allows the mandrake to wander around the scene using a navmeshagent
    public void MandrakeRoaming()
    {
        if (time <= 0)
        {
            Vector3 newPos = RandomNavSphere(mandrake.transform.position, wanderRange, -1);
            agent.SetDestination(newPos);
            agent.isStopped = false;
            time = 0;
            state = Mandrake.Roam;
        }
        //when the mandrake gets caught it moves back to its original idle pos.
        else if(caught)
        {
            caught = false;
            time = 15f;
            agent.SetDestination(idlePos.transform.position);
            state = Mandrake.Idle;
        }
    }

    //method that returns a random point on the navmesh to move towards from the mandrake's pos.
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;

    }

}