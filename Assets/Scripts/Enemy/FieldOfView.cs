using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public FollowObject followObject;

    void OnTriggerEnter(Collider col)
    {
        //print("Collided:" + col.name);
        if(col.name.Contains("Avatar"))
        {
           followObject.ChangeTarget(col.gameObject); 
        }
    }
}
