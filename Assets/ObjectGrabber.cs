using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public GameObject rightHand;
    private bool isGrabbingGun;

    public PCInteractionProvider pCInteractionProvider;
    public CustomBaseControllerManager customBaseControllerManagerRight;

    void OnTriggerEnter(Collider col)
    {
        print("Grab Detector:" + col.name);

        if(col.name.Contains("Gun") && !isGrabbingGun)
        {
            isGrabbingGun = true;

            //Adjusting position and parenting
            col.transform.position = rightHand.transform.position + new Vector3(0f, -0.2f, 0.3f);
            col.transform.rotation = Quaternion.Euler(rightHand.transform.eulerAngles.x,rightHand.transform.eulerAngles.y,rightHand.transform.eulerAngles.z);
            col.transform.parent   = rightHand.transform;

            //Get the gun properties
            col.gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        

            if(!CurrentPlatformManager.instance.IsOnQuest())
            {
                //Attaching the gun to the player on PC
                pCInteractionProvider.AttachWeapon(col.gameObject);
            }
            else
            {
                //Attaching the gun to the player hand
                customBaseControllerManagerRight.AttachWeapon(col.gameObject);
            }
            
        }

        print("Trigger: "+col.name);
    }
}
