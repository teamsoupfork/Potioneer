using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SageInteract : MonoBehaviour
{
    public static bool SageTouched;

    public void OnTriggerEnter(Collider other)
    {
        DFPlayer.SageTouched = true;
    }

    public void OnTriggerExit(Collider other)
    {
        DFPlayer.SageTouched = false;
    }
}
