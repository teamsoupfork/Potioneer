using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    /// <summary>
    /// Created, referenced and adapted by Chung Ling Kristy
    /// <ref>https://answers.unity.com/questions/886884/drag-and-drop-with-perspective-camera-3d.html</ref>
    /// </summary>
    /// 
    Vector3 startPos;
    bool boxPos;

    private bool dragging = false;
    private float distance;
    public Animator BoxingAnim;
    public Collider CustomerWantCollider;
    AudioSource potionClink;
    public AudioClip clip1, clip2;
  
    void Start()
    {
        startPos = gameObject.transform.position; //sets original position of the object
        BoxingAnim = GetComponent<Animator>();   //find the Animator compoenent on the gameObject
        potionClink = GetComponent<AudioSource>(); //find the Audio Source on the GameObject
        if (this.CompareTag("Red"))
        {
            CustomerWantCollider = GameObject.Find("WantColliderRed").GetComponent<Collider>();
        }
        if (this.CompareTag("Blue"))
        {
            CustomerWantCollider = GameObject.Find("WantColliderBlue").GetComponent<Collider>();
        }

    }
    void OnMouseDown()
    {
        potionClink.clip = clip1;
        potionClink.Play();
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        potionClink.clip = clip2;
        potionClink.Play();
        dragging = false;
        if(!boxPos)
        {
            transform.position = startPos;
        }
    }

    void Update()
    {
        if (dragging)
        {
            distance = Vector3.Distance(new Vector3(transform.position.x, startPos.y, transform.position.z), Camera.main.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            boxPos = true;
            BoxingAnim.SetBool("Boxed", true);
        }

        if (CompareTag("Red"))
        {
            if (other.gameObject.CompareTag("boxRed"))
            {
                boxPos = true;
                BoxingAnim.SetBool("Boxed", true);
                PotionStorage.NumberOfRedPotions--;
                WaitToDestroy(2);
                CustomerInteraction.received = true;
                Debug.Log("Red Potion given");
            }
        }
        if (CompareTag("Blue"))
        {
            if (other.gameObject.CompareTag("boxBlue"))
            {
                boxPos = true;
                BoxingAnim.SetBool("Boxed", true);
                PotionStorage.NumberOfBluePotions--;
                WaitToDestroy(2);
                CustomerInteraction.received = true;
                Debug.Log("Blue Potion given");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            if (CompareTag("Blue"))
            {          
                WaitToDestroy(2);
                Debug.Log("Blue Potion given");
            }
            if (CompareTag("Red"))
            {
                WaitToDestroy(2);
                Debug.Log("Red Potion given");
            }
        }
    }

    IEnumerator WaitToDestroy(int i)
    {
        if (CompareTag("Blue"))
        {
            PotionStorage.NumberOfBluePotions--;
        }
        else if (CompareTag("Red"))
        {
            PotionStorage.NumberOfRedPotions--;
        }
        CustomerInteraction.received = false;
        yield return new WaitForSeconds(i);
        Debug.Log("Destroyed" + gameObject.name);
        Destroy(gameObject);
        yield return null;
    }
}
