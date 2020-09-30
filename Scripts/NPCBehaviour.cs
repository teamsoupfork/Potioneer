using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour
{
    /// <summary>
    /// Allows player to see new locations when talking to townsfolk
    /// </summary>
    public GameObject player, 
                        SignRes1, SignRes2, SignRes3, SignPriRes, 
                        NoticeBoard, NoticeBoardTxt, particlePop;
    public static bool check;

    private void Start()
    {
        //retrieves player
        player = GameObject.Find("Player");

        //sets all signs to false so the player doesn't know the locations immediately upon starting
        SignRes1.SetActive(false);
        SignRes2.SetActive(false);
        SignRes3.SetActive(false);
        SignPriRes.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !check)
        {
            if (this.gameObject.name == "ResBlk1NPC")
            {
                StartCoroutine(Noticed("House 1", SignRes1));            
            }            
            if (this.gameObject.name == "ResBlk2NPC")
            {
                StartCoroutine(Noticed("House 2", SignRes2));             
            }            
            if (this.gameObject.name == "ResBlk3NPC")
            {
                StartCoroutine(Noticed("House 3", SignRes3));             
            }            
            if(this.gameObject.name == "PriResNPC")
            {
                StartCoroutine(Noticed("Private House", SignPriRes));
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !check)
        {
            //prevents the player from 'rediscovering' known locations
            this.GetComponent<CapsuleCollider>().enabled = false;
        }           
    }

    IEnumerator Noticed(string name, GameObject board) //using name of location and sign, instantiates particles as well as making the sign appear
    {
        //announces to the player that they have discovered the location
        NoticeBoard.SetActive(true);
        NoticeBoardTxt.GetComponent<Text>().text = name;
        board.SetActive(true);

        //instantiates particles to catch the players attention
        GameObject newP = Instantiate(particlePop, board.transform);
        newP.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(3);

        //after the set time hides the noticeboard and destroys the particle system
        NoticeBoard.SetActive(false);
        Destroy(newP);
    }
}
