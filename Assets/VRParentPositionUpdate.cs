using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRParentPositionUpdate : MonoBehaviour
{
    public GameObject playerParent;
    
    void Update()
    {
        playerParent.transform.position = gameObject.transform.position;
    }
}
