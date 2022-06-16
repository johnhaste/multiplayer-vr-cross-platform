using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpherePlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    //Scripts for PC
    public Camera camera;
    public PCInteractionProvider pCMoveProvider;
    public CameraMouseMovement cameraMouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            camera.enabled = true;
            pCMoveProvider.enabled = true;
            cameraMouseMovement.enabled = true;
        }
        else
        {
            camera.enabled = false;
            pCMoveProvider.enabled = false;
            cameraMouseMovement.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
