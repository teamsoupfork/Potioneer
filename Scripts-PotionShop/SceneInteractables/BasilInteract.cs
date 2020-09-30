using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasilInteract : MonoBehaviour
{
    public static bool BasilTouched;

    public void OnTriggerEnter(Collider other)
    {
        DFPlayer.BasilTouched = true;
    }

    public void OnTriggerExit(Collider other)
    {
        DFPlayer.BasilTouched = false;
    }
}
