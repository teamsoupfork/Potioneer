using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerInteraction : MonoBehaviour
{
    /// <summary>
    /// <ref>
    /// 
    /// </ref>
    /// </summary>

    public bool BluePotion, RedPotion;

    public GameObject fly, clone;
    public Collider blueCollider, redCollider;

    public static bool BlueWanted, RedWanted, blueBool, redBool, received, paid;

    public List<GameObject> customers;


    void Awake()
    {
        blueCollider = GameObject.Find("WantColliderBlue").GetComponent<Collider>();
        redCollider = GameObject.Find("WantColliderRed").GetComponent<Collider>();
    }

    void Update()
    {
        if (gameObject == customers[0])
        {
            RedWanted = true;
            BlueWanted = false;
        }
        else if (gameObject == customers[1])
        {

            BlueWanted = true;
            RedWanted = false;
        }

        if (redBool)
        {
            redCollider.enabled = true;
            CustomerPay();
        }
        else if (blueBool)
        {
            blueCollider.enabled = true;
            CustomerPay();
        }
    }

    void CustomerPay()
    {
        if (blueBool && received)
        {
            CustomerFrequency.tutorialdone = true;
            BluePotion = true;
            received = false;
            PotionChecker();
        }
        if (redBool && received)
        {
            RedPotion = true;
            received = false;
            PotionChecker();
        }       
    }
    void PotionChecker()
    {
        if (BluePotion && BlueWanted)
        {
            CustomerPath.good = true;
            Reset();
        }
        else if (RedPotion && RedWanted)
        {
            CustomerPath.good = true;
            Reset();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "WantCollider")
        {
            if (gameObject.name == "Customer1(Clone)")
            {
                redBool = true;
            }
            else if (gameObject.name == "Customer2(Clone)")
            {
                blueBool = true;
            }
        }
    }

    private void Reset()
    {
        BlueWanted = false;
        RedWanted = false;
        blueBool = false;
        redBool = false;
        received = false;
        paid = false;
    }
}
