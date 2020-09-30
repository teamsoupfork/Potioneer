using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailPlayer : MonoBehaviour
{
    /// <summary>
    /// Allows collection of ingredients and adds a counter to the ingredients in the Shop
    /// </summary>
    public GameObject player, Obj, m;
    public static int BasilCount, SageCount;
    public static bool Colbasil, Colsage;
    public GameObject Basil, Sage, bParticle, sParticle, 
                        NoticeBoard, NoticeBoardTxt;

    private void Update()
    {
        //When 5 units away from herb, player will pick up herb
        if (Vector3.Distance(player.transform.position, Obj.transform.position) <= 5) 
        {
            if (Obj.gameObject.name == "Basil")
            {
                StartCoroutine(Obtained("Basil"));
                Colbasil = true;

                //Parented to player character to show the collection of Herb
                m = GameObject.Find("Moving1");
                Obj.transform.parent = m.transform;
                Obj.transform.position = m.transform.position;

                //Player collects 3 herbs in bundle
                BasilCount = 4;

                TimeManager.currBal -= 10f; //deducts $10 of cash from the wallet
                bParticle.SetActive(false); //hides the highlighted particle
            }
            else if (Obj.gameObject.name == "Sage")
            {
                StartCoroutine(Obtained("Sage"));
                Colsage = true;

                //Parented to player character to show the collection of Herb
                m = GameObject.Find("Moving2");
                Obj.transform.parent = m.transform;
                Obj.transform.position = m.transform.position;

                //Player collects 3 herbs in bundle
                SageCount = 3;

                TimeManager.currBal -= 15f; //deducts $15 of cash from the wallet
                sParticle.SetActive(false); //hides the highlighted particle
            }
        }
    }


    //using name of location and sign, instantiates particles as well as making the sign appear
    IEnumerator Obtained(string name) 
    {
        NoticeBoard.SetActive(true);
        NoticeBoardTxt.GetComponent<Text>().text = name;
        yield return new WaitForSeconds(3);
        NoticeBoard.SetActive(false);
    }
}
