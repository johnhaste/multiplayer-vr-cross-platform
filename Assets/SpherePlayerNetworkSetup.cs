using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpherePlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    //Scripts for PC
    public Camera playerCamera;
    public PCInteractionProvider pCMoveProvider;
    public CameraMouseMovement cameraMouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            playerCamera.enabled = true;
            pCMoveProvider.enabled = true;
            cameraMouseMovement.enabled = true;

            gameObject.AddComponent<AudioListener>();
        }
        else
        {
            //Change their layers so the local player can see other people's bodies
            SetLayerRecursively(gameObject, 12 );
            SetLayerRecursively(gameObject, 12 );

            playerCamera.enabled = false;
            pCMoveProvider.enabled = false;
            cameraMouseMovement.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
