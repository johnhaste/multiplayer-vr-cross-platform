using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        print("Attack Detector:" + col.name);
    }

}
