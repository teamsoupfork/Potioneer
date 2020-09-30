using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScript : MonoBehaviour
{
    /// <summary>
    /// Takes charge of the phone's accessability and controls
    /// </summary>
    public GameObject phone, GPS, wallet, pocket, orderMenu, orderIcon, GPSApp, GPSIcon, MandyApp;

    Animator phoneAnim, GPSAnim, WalletAnim;

    public Text balance, confirmTxt;

    public static bool call, start, check, delayed, order, orderConfirm;

    void Start()
    {
        //Get Animators for different elements(phone and phoneUI)
        phoneAnim = phone.GetComponent<Animator>();
        GPSAnim = GPS.GetComponent<Animator>();
        WalletAnim = wallet.GetComponent<Animator>();

        balance = pocket.GetComponentInChildren<Text>();

        call = true;
        start = true;
        check = true;

        //player is late to leave, delayed would be set true
        if (delayed)
        {
            GPSIcon.GetComponentInChildren<Button>().interactable = false;
        }
        //once confirmed, the player can't change the minimum CustomerCount requirement after leaving the scene
        if (orderConfirm)
        {
            orderIcon.GetComponent<Button>().interactable = false;
        }
        balance.text = TimeManager.currBal.ToString();
    }
    
    void Update()
    {
        //allows the player to check their wallet funds
        balance.text = "$" + TimeManager.currBal.ToString();
    }

    public void Clicked() //For Phone in Delegation, animations slide the phone up and down from player's view
    {
        if (call)
        {
            phoneAnim.Play("Up");
            call = false;
        }
        else
        {
            phoneAnim.Play("Down");
            call = true;
        }
    }

    public void Clicked2() //For Phone in Slider, animations slide the phone up and down from player's view
    {
        Debug.Log("Ive been clicked");
        if (call)
        {
            phoneAnim.Play("Lift");
            call = false;
        }
        else
        {
            phoneAnim.Play("Fall");
            call = true;
        }
    }

    public void MenuGPS() //For Phone in Delegation, animations for player to view the locations they can set on the GPS wayfinder
    {
        if (start)
        {
            GPSAnim.Play("SlideDown");
            start = false;
        }
        else
        {
            GPSAnim.Play("SlideUp");
            start = true;
        }
    }

    public void WalletCheck() //For Phone in Delegation and Shop Scene, animations for displaying wallet balance
    {
        if (check)
        {
            WalletAnim.Play("Check");
            check = false;
        }
        else
        {
            WalletAnim.Play("CheckNot");
            check = true;
        }
    }

    public void CustomerOrder() //enables the OrderApp and disables GPS
    {
        GPSApp.SetActive(false);
        orderMenu.SetActive(true);
    }
    public void CheckGPS() //enables GPSApp and disables OrderApp
    {
        GPSApp.SetActive(true);
        orderMenu.SetActive(false);
    }

    public void Order()
    {
        Debug.Log("Ive been clicked");
        if (order)
        {
            orderMenu.SetActive(true);
            order = false;
        }
        else
        {
            orderMenu.SetActive(false);
            order = true;
        }
    }

    /// <summary>
    /// Buttons that increase and decrease value in one function
    /// <ref>https://forum.unity.com/threads/increase-and-decrease-text-text-values.417865/</ref>
    /// </summary>

    public Text text;
    public Button btnDecrease; // added reference to buttons so can disable them
    public Button btnIncrease;

    public int max = 7;
    public int min = 3;

    public static int currentAmount = 3;
    int increasePerClick = 1;

    // from button click event, call AdjustValue(true) if want to increase or AdjustValue(false) to decrease
    public void AdjustValue(bool increase)
    {
        // clamp current value between min-max
        currentAmount = Mathf.Clamp(currentAmount + (increase ? increasePerClick : -increasePerClick), min, max); // ? means IF TRUE THEN , : means ELSE
        text.text = currentAmount.ToString();

        // disable buttons if cannot increase/decrease
        btnDecrease.interactable = currentAmount > min;
        btnIncrease.interactable = currentAmount < max;
    }

    public void Confirmed() //after changing the number 
    {
        confirmTxt.text = "Confirmed";
        orderIcon.GetComponent<Button>().interactable = false;
        TimeManager.initCustomerCount = currentAmount;
        orderMenu.SetActive(false);
        orderConfirm = true;
    }

    public void LeaveApp() //Closes all apps when home button is pressed
    {
        orderMenu.SetActive(false);
        GPSApp.SetActive(false);
        MandyApp.SetActive(false);
        order = true;
        start = true;
        confirmTxt.text = "Confirm";
    }
}

