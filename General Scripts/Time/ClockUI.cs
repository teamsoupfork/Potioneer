using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    /// <ref>
    /// https://www.youtube.com/watch?v=pbTysQw-WNs
    /// </ref>
    public Transform clockHandTransform;
    public GameObject SecClock;
    public float IRLSecondsPerGameDay = 300f;
    public Light directionalLight;
    public float TimeofDay;
    public static bool paused, hideSec;

    public Animator ClockAnim;

    private void Awake()
    {
        clockHandTransform = transform.Find("HandTransform");
        TimeofDay = TimeManager.ShopTime;
    }

    // Update is called once per frame
    void Update()
    {
        MainClock();
        if(this.name == "MainClock")
        {
            StartCoroutine(Wait(5));
        }
        if (hideSec)
        {
            ClockAnim.SetBool("HideSec", true);
            SecClock.SetActive(false);     
        }
    }

    void MainClock()
    {
        if (TimeManager.ShopTime < 1f && !paused) //within the parameter of the in game day
        {
            TimeManager.ShopTime += Time.deltaTime / IRLSecondsPerGameDay;
            float ShopTime_Normalised = TimeManager.ShopTime % 1f; //keeps the value between 0 and 1
            float rotationDegreesPerDay = 360f;
            if(this.name == "ClockFace")
            {
                clockHandTransform.eulerAngles = new Vector3((float)61.829, 0, -ShopTime_Normalised * rotationDegreesPerDay); //Sets rotation of the clockhand to an ingame day for Shop
            }
            else if(this.name == "MainClock")
            {
                clockHandTransform.eulerAngles = new Vector3(0, 0, -ShopTime_Normalised * rotationDegreesPerDay); //Sets rotation of the clockhand to an ingame day for Delegation
            }
            
        }
        else if(TimeManager.ShopTime >= 1 && !paused)
        {
            TimeManager.EndofDay = true;
            Time.timeScale = 0f; //stops everything    
        }
        if (TimeManager.ShopTime > .25f && TimeManager.ShopTime < 0.5f)
        {
            //Debug.Log("Morning Over");

            directionalLight.color = new Color(0.9f, 0.9f, 0.9f, 0.75f);
        }
        else if (TimeManager.ShopTime > .75f && TimeManager.ShopTime < 1f)
        {
            //Debug.Log("Afternoon Over");
            directionalLight.color = new Color(0.55f, 0.55f, 0.55f, 0f);
        }
    }

    //hides text in delegation scene
    IEnumerator Wait(int i)
    {
        yield return new WaitForSeconds(i);
        ClockAnim.SetBool("HideMain", true);
    }
}
