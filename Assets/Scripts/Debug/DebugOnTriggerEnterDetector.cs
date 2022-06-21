using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnTriggerEnterDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        //print("Grab Detector:" + col.name);
    }
}
