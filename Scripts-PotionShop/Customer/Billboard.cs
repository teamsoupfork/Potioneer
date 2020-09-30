using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{
    public Camera cam;
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        if(this.name == "Broom")
        {
            transform.LookAt(cam.transform.position, Vector3.up);
        }
    }
}
