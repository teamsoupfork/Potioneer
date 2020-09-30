using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoint : MonoBehaviour
{
    /// <summary>
    /// Shows the location with an icon
    /// <ref>https://www.youtube.com/watch?v=oBkfujKPZw8</ref>
    /// </summary>
    /// 

    public Image WPShp, WPFrm, WPMkt;
    public Transform TargetShp, TargetFrm, TargetMkt;
    public Text meterShp, meterFrm, meterMkt;

    Vector3 offset;
    void Start()
    {
        offset = new Vector3(0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetWP();
    }

    void SetWP()
    {
        //sets the WP graphic on the actual Target in the map
        WPShp.transform.position = Camera.main.WorldToScreenPoint(TargetShp.position + offset);
        meterShp.text = ((int)Vector3.Distance(TargetShp.position, transform.position) - 15).ToString() + "m";
        
        WPFrm.transform.position = Camera.main.WorldToScreenPoint(TargetFrm.position + offset);
        meterFrm.text = ((int)Vector3.Distance(TargetFrm.position, transform.position) - 15).ToString() + "m";

        WPMkt.transform.position = Camera.main.WorldToScreenPoint(TargetMkt.position + offset);
        meterMkt.text = ((int)Vector3.Distance(TargetMkt.position, transform.position) - 15).ToString() + "m";
    }
}
