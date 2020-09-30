using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryServiceScript : MonoBehaviour
{
    /// <summary>
    /// Controls events in the delivery shelf
    /// </summary>
    public GameObject DeliveryBtn, DeliveryBox,
                        Order1, Order2, Order3, Order4;

    public static GameObject Box1, Box2;

    public static int Box1CounterB, Box1CounterR,
                        Box2CounterB, Box2CounterR = 0;

    bool open;

    void Start()
    {
        DeliveryBox.GetComponent<Animator>().Play("SlideRight");
    }

    // Update is called once per frame
    void Update()
    {
        //sets 4 orders in the menu
        Order(Order1,"House 1", 15, 1, 0);
        Order(Order2,"House 3", 20, 2, 1);
        Order(Order3,"House 2", 15, 1, 1);
        Order(Order4,"Private House", 50, 1, 3);
    }

    void Order(GameObject locator,string location, int price, int quantityB, int quantityR)
    {
        locator.transform.GetChild(0).GetComponent<Text>().text = location;
        locator.transform.GetChild(1).GetComponent<Text>().text = "$" + price.ToString();
        if(quantityB != 0)
        {
            locator.transform.GetChild(2).GetComponent<Text>().text = "x" + quantityB.ToString();
        }
        else
        {
            locator.transform.GetChild(2).GetComponent<Text>().text = "x0";
        }
        if (quantityR != 0)
        {
            locator.transform.GetChild(3).GetComponent<Text>().text = "x" + quantityR.ToString();
        }
        else
        {
            locator.transform.GetChild(3).GetComponent<Text>().text = "x0";
        }
    }

    public void OpenClosePanel()
    {
        if (open)
        {
            DeliveryBox.GetComponent<Animator>().Play("SlideLeft");
            open = false;
        }
        else
        {
            DeliveryBox.GetComponent<Animator>().Play("SlideRight");
            open = true;
        }
    }

}
