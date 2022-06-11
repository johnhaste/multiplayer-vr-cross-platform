using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutsideLayout : MonoBehaviour
{
    
    void OnTriggerEnter(Collider col)
    {
        if(col.name == "InvisibleWall")
        {
            Destroy(gameObject);
        }
    }

}
