using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartImage : MonoBehaviour
{
    public Image StartIMG;
    public Sprite[] Images;
    // Start is called before the first frame update
    void Start()
    {
        ChangeImage();
        StartIMG = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeImage()
    {
        if (Random.Range(1, 10) >= 7)
        {
            StartIMG.sprite = Images[0];

        }
        else
        {
            StartIMG.sprite = Images[1];
        }
    }
}
