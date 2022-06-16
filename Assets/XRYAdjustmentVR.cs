using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRYAdjustmentVR : MonoBehaviour
{

    public GameObject XRRig;

    // Start is called before the first frame update
    void Start()
    {
        //Check if it's on PC or Quest
        if(!CurrentPlatformManager.instance.IsOnQuest())
        {
            print("Adjust");
            XRRig.transform.position = new Vector3(0f,3f,-1.5f);
        }
    }
}
