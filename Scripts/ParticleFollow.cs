using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    /// <summary>
    /// Creates and tracks path for the player to follow to each destination
    /// </summary>
    public string PathName;
    public float time;

    public void ClickShp()
    {
        PathName = "ShpPath";
        Go();
        PhoneScript.start = false;
    }
    public void ClickFrm()
    {
        PathName = "FrmPath";
        Go();
        PhoneScript.start = false;
    }
    public void ClickMkt()
    {
        PathName = "MktPath";
        Go();
        PhoneScript.start = false;
    }

    //the path repeats for the player to follow
    void Go()
    {
        var i = iTween.LoopType.loop;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "easetype", iTween.EaseType.easeInOutSine, "time", time, "loopType", i)) ; //object moves along path using an easetype      
    }
}
