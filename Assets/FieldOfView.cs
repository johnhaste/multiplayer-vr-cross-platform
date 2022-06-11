using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public FollowObject followObject;

    void OnTriggerEnter(Collider col)
    {
        print("COllided:" + col.name);
        if(col.name.Contains("Player"))
        {
           followObject.ChangeTarget(col.gameObject); 
        }
    }
  
}
