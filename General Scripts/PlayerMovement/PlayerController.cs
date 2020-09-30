using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Controls the Player 
/// </summary>

///<ref>
///https://www.youtube.com/watch?v=8-X3BmvtXT0
///</ref>
/// adapted by Gabriel Lek
public class PlayerController : MonoBehaviour
{
    protected Joystick joyStick;
    protected Joybutton joyButton;

    private Vector3 pointA, pointB;
    public Transform cPlayer;

    public ParticleSystem PlayerTrail;

    public LevelChanger Transitions;
    private void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<Joybutton>();        
    }
    private void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
            
        if (pointB - pointA != Vector3.zero)
        {
            MoveCharacter();
            Quaternion targetRotation = Quaternion.LookRotation(pointB - pointA);
            float turnSpeed = 3f;
            cPlayer.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                                                    Camera.main.transform.position.z));
            
        }
        if (Input.GetMouseButton(0))
        {
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                                                    Camera.main.transform.position.z));
           
        }

        if (rigidbody.velocity.magnitude > 0)
        {
            CreateTrail();
        }
        else
        {
            rigidbody.velocity = new Vector3(0,0,0);
            PlayerTrail.Stop();
        }

    }
    void MoveCharacter()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(joyStick.Horizontal * 10f, 0, joyStick.Vertical * 10f); //this is what allows the character to move.
    }
    void CreateTrail()
    {
        if (!PlayerTrail.isPlaying)
        {
            PlayerTrail.Play();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "LeaveShop")
        {
            StartCoroutine(LeaveShop(2));
        }
    }

    IEnumerator LeaveShop(int i)
    {
        yield return new WaitForSeconds(i);
        Transitions.FadeToPreviousLevel();
        WayPointController.change = true;
    }
}
