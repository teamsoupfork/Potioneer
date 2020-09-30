using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandrakeInteract : MonoBehaviour
{
    public static bool MandrakeTouched;

    public void OnTriggerEnter(Collider other)
    {
        DFPlayer.MandrakeTouched = true;
    }

    public void OnTriggerExit(Collider other)
    {
        DFPlayer.MandrakeTouched = false;
    }
}
