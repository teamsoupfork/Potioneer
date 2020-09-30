using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WayPointController : MonoBehaviour
{
    /// <summary>
    /// Controls main events in the scene
    /// </summary>
    public GameObject b1, Hollow, WP, player, box, DeliveryBtn,
                        shop, scroll;
    public Collider r1, r2, r3, pr;

    public static bool delayed, change, morning, readyForDelivery;

    private bool deliveredPotion, canThrow;

    
    private void Awake()
    {
        //Sets button to deliver boxes
        DeliveryBtn.SetActive(false);
        HavePotions();
        //sets the player position to be in front of shop
            player.transform.position = shop.transform.position;
      
      
        if (morning || !readyForDelivery)
        {
         r1.enabled = false;
         r2.enabled = false;
         r3.enabled = false;
         pr.enabled = false;
         scroll.SetActive(false);
        }
        else if (readyForDelivery)
        {
          r1.enabled = true;
          r2.enabled = true;
          r3.enabled = true;
          pr.enabled = true;
          scroll.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //inefficient way to call the different collider
         if (other.gameObject.name == "Shop")
        {
            Debug.Log("Gone to Shop");
            StartCoroutine(StartShop(2)); //Goes to ShopScene after 2 second pause
        }

        //Sets the deliveries for specific Residential blocks
        else if (other.gameObject.name == "ResBlk1")
        {
            Debug.Log("Gone to ResBlk1");
            WP = GameObject.Find("WP1");
            canThrow = true;
        }
        else if (other.gameObject.name == "ResBlk2")
        {
            Debug.Log("Gone to ResBlk2");
            WP = GameObject.Find("WP2");
            canThrow = true;
        }
        else if (other.gameObject.name == "ResBlk3")
        {
            Debug.Log("Gone to ResBlk3");
            WP = GameObject.Find("WP3");
            canThrow = true;
        }
        else if (other.gameObject.name == "PriRes")
        {
            Debug.Log("Gone to PriRes");
            WP = GameObject.Find("WPPri");
            canThrow = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "ResBlk1")
        {
            if (deliveredPotion)
            {
                GameObject del = GameObject.Find("ResBlk1Txt");
                Destroy(del);
                canThrow = false;
                deliveredPotion = false;
                Destroy(other.GetComponent<Collider>());
                
            }

        }
        else if (other.gameObject.name == "ResBlk2")
        {
            if (deliveredPotion)
            {
                GameObject del = GameObject.Find("ResBlk2Txt");
                Destroy(del);
                canThrow = false;
                deliveredPotion = false;
                Destroy(other.GetComponent<Collider>());
               
            }
        }
        else if (other.gameObject.name == "ResBlk3")
        {
            if (deliveredPotion)
            {
                GameObject del = GameObject.Find("ResBlk3Txt");
                Destroy(del);
                canThrow = false;
                deliveredPotion = false;
                Destroy(other.GetComponent<Collider>());
               
            }
        }
        else if (other.gameObject.name == "PriRes")
        {
            if (deliveredPotion)
            {
                GameObject del = GameObject.Find("PriResTxt");
                Destroy(del);               
                canThrow = false;
                deliveredPotion = false;
                Destroy(other.GetComponent<Collider>());
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canThrow = false;
    }

    private void Update()
    {
        if (DeliveryBoxCounter.BoxToDel != 0)
        {
            canThrow = true;
            DeliveryBtn.SetActive(true);
        }
        else if(!canThrow)
        {
            DeliveryBtn.SetActive(false);
        }
       
    }
    IEnumerator StartShop(int i)
    {
        yield return new WaitForSeconds(i);
        SceneManager.LoadScene("ShopScene");
        change = true;
    }

    public void ThrowBox()
    {
        if (canThrow)
        {
            Vector3 offset = new Vector3(0, 5, 0);
            Vector3 direction = player.transform.position - WP.transform.position;
            direction.Normalize();
            Instantiate(box, this.transform.position + offset, Quaternion.identity);
            Rigidbody rb = box.GetComponent<Rigidbody>();
            rb.AddForce(direction, ForceMode.Impulse);
            deliveredPotion = true;
            TimeManager.TempTime = 0f;
            TimeManager.extraTime++;
            DeliveryBoxCounter.BoxToDel--;
        }
        else
        {
            deliveredPotion = false;
            Debug.Log("Button shouldn't be active");
        }
    }

    public void HavePotions()
    {
        if (DeliveryBoxCounter.BoxToDel > 0)
        {
            Debug.Log("ready for delivery");
            readyForDelivery = true;
        }
        else if (DeliveryBoxCounter.BoxToDel <= 0)
        {
            Debug.Log("cant deliver no more");
            readyForDelivery = false;
           
        }   
    }
}
