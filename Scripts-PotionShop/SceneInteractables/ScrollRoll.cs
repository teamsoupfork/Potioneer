using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRoll : MonoBehaviour
{
    public Button Scroll;
    public Sprite open, close;
    public bool rolled = true;
    public GameObject b1, Goal;

    
    public  bool ObjectiveShop, ObjectiveDeliver;
    private void Start()
    {
        if (ObjectiveShop)
        {
            Goal.GetComponent<Text>().text = "Get to Shop";
        }
        else if (ObjectiveDeliver)
        {
            Goal.GetComponent<Text>().text = "Orders To Deliver";
        }
        b1.SetActive(false);
    }
    public void OnClick()
    {
        if (rolled)
        {
            Scroll.GetComponent<Image>().sprite = close;
            Scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 400);
            b1.SetActive(false);
            rolled = false;
        }
        else if (!rolled)
        {
            Scroll.GetComponent<Image>().sprite = open;
            Scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 500);
            b1.SetActive(true);
            rolled = true;
        }
    }
}
