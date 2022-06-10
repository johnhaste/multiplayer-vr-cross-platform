using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectsDetector : MonoBehaviour
{

    public GameObject rightHand;
    private bool isGrabbingGun;

    void OnTriggerEnter(Collider col)
    {

        if(col.name == "GunGrab" && !isGrabbingGun)
        {
            isGrabbingGun = true;

            col.transform.position = rightHand.transform.position + new Vector3(0f, 0f, 0.3f);
            col.transform.rotation = Quaternion.Euler(rightHand.transform.eulerAngles.x,rightHand.transform.eulerAngles.y,rightHand.transform.eulerAngles.z);
            col.transform.parent = rightHand.transform;

            //Get the gun properties
            col.gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        print("Trigger: "+col.name);
    }
}
