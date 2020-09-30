using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildNPCBehavior : MonoBehaviour
{
    private int destPoint = 0;
    public GameObject child;

    public UnityEngine.AI.NavMeshAgent agent;
    GameObject[] points;

    void Start()
    {

        points = GameObject.FindGameObjectsWithTag("Waypoint");
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //if there are no waypoints set
        if (points.Length == 0)
        {
            return;
        }
        if (Vector3.Distance(points[destPoint].transform.position, child.transform.position) < 3)
        {
            destPoint++;
            if (destPoint >= points.Length)
            {
               destPoint = 0;
            }
        }
        agent.SetDestination(points[destPoint].transform.position);
       
    }
}
