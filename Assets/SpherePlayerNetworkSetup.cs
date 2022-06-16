using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpherePlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    //XR Rig
    public GameObject LocalXRRigGameobject;

    //Avatar Body Parts
    public GameObject MainAvatarGameObject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
    public GameObject AvatarLeftHandGameObject;
    public GameObject AvatarRightHandGameObject;
    public GameObject[] AvatarModelPrefabs;

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

            //Loading correct avatar model
            object avatarSelectionNumber;
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                print("Avatar selection number:" + (int) avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int) avatarSelectionNumber);
            }

            gameObject.AddComponent<AudioListener>();
        }
        else
        {
            //Change their layers so the local player can see other people's bodies
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

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber], gameObject.transform);

        AvatarInputConverter avatarInputConverter = LocalXRRigGameobject.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();

        if(photonView.IsMine)
        {
            avatarHolder.SetLayersForOwn();
        }
        else
        {
            SetLayerRecursively(avatarHolder.Head, 12);
            SetLayerRecursively(avatarHolder.Body, 12);
        }
        

        SetUpAvatarGameobject(avatarHolder.HeadTransform,avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform,avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
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
