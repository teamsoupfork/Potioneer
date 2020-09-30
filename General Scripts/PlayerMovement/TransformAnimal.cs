using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimal : MonoBehaviour
{
    public GameObject OptionsMenu, Cat, Penguin;
    public static bool catBool, penguinBool;

    private void Start()
    {
        if (catBool)
        {
            Cat.SetActive(true);
            Penguin.SetActive(false);
        }
        else if (penguinBool)
        {
            Penguin.SetActive(true);
            Cat.SetActive(false);
        }
        else
        {
            Cat.SetActive(true);
            Penguin.SetActive(false);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            ClockUI.paused = true;
            OptionsMenu.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            ClockUI.paused = false;
            OptionsMenu.SetActive(false);
        }
    }

    public void CatTransform()
    {
        Cat.SetActive(true);
        Penguin.SetActive(false);
        OptionsMenu.SetActive(false);
        catBool = true;
    }

    public void PenguinTransform()
    {
        Penguin.SetActive(true);
        Cat.SetActive(false);
        OptionsMenu.SetActive(false);
        penguinBool = true;
    }
}
