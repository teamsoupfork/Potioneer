using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryBoxCounter : MonoBehaviour
{
    /// <summary>
    /// Tracks the number of potions stored in box
    /// </summary>
    public Text BlueNo, RedNo;
    public int CounterB, CounterR;
    public static int BoxToDel;
    public Image closed, open, currImg;
    public static bool TapeUp;
    public Button TapeBut;
    public List<GameObject> Box;
    public int red, blue;

    public void Start()
    {
        CounterB = 0;
        CounterR = 0;
        BoxToDel = 0;
        BlueNo.text = "x " + CounterB;
        RedNo.text = "x " + CounterR;
        TapeBut.gameObject.SetActive(false);
    }

    private void Update()
    {
        BlueNo.text = "x " + CounterB;
        RedNo.text = "x " + CounterR;
        if(CounterB > 0|| CounterR > 0)
        {
            TapeBut.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            CounterB++;
        }
        if (other.gameObject.CompareTag("Red"))
        {
            CounterR++;
        }
    }

    public void Tape()
    {
        this.gameObject.SetActive(false);
        BoxToDel++;
        this.GetComponent<Collider>().enabled = false;
    }
}
