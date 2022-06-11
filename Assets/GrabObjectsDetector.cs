using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectsDetector : MonoBehaviour
{

    public GameObject rightHand;
    private bool isGrabbingGun;

    public PCInteractionProvider pCInteractionProvider;

    void OnTriggerEnter(Collider col)
    {

        if(col.name == "Gun" && !isGrabbingGun)
        {
            isGrabbingGun = true;

            //Adjusting position and parenting
            col.transform.position = rightHand.transform.position + new Vector3(0f, -0.2f, 0.3f);
            col.transform.rotation = Quaternion.Euler(rightHand.transform.eulerAngles.x,rightHand.transform.eulerAngles.y,rightHand.transform.eulerAngles.z);
            col.transform.parent   = rightHand.transform;

            //Get the gun properties
            col.gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
            //Attaching the fun to the player
            pCInteractionProvider.AttachWeapon(col.gameObject);
        }

        print("Trigger: "+col.name);
    }
}
