using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SecClockUI : MonoBehaviour
{
    /// <ref>
    /// https://www.youtube.com/watch?v=pbTysQw-WNs
    /// </ref>
    public Transform secClockHandTransform;
    public float IRLSecondsPerGameDay = 25f;
    public float TimeofDay;

    public static bool notDeliveredOnTime;

    private void Awake()
    {
        secClockHandTransform = transform.Find("SecClockHand");
        TimeofDay = TimeManager.TempTime;
    }

    // Update is called once per frame
    void Update()
    {
        SecClock();
    }

    void SecClock()
    {
        if (TimeManager.TempTime < 1f && !notDeliveredOnTime) //within the parameter of the in game day
        {
            TimeManager.TempTime += Time.deltaTime / IRLSecondsPerGameDay;
            float TempTime_Normalised = TimeManager.TempTime % 1f; //keeps the value between 0 and 1
            //Debug.Log(TimeManager.ShopTime);
            float rotationDegreesPerDay = 360f;
            secClockHandTransform.eulerAngles = new Vector3(0, 0, -TempTime_Normalised * rotationDegreesPerDay); //Sets rotation of the clockhand to an ingame day
            //extra time is given within submission of product in this time frame, using OnClick from the deliveryBtn it will reset SecClock time as well as add to a deliveryCounter(used to determine extra time given)
        }
        else
        {
            notDeliveredOnTime = true;
            //no extra time
        }
    }
}

