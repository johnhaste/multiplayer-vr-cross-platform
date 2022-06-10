using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    //XR Rig
    public GameObject LocalXRRigGameobject;
    
    //Avatar Body Parts
    public GameObject MainAvatarGameObject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
    public GameObject AvatarLeftHandGameObject;
    public GameObject AvatarRightHandGameObject;
    public GameObject AvatarFullBody;
    public GameObject[] AvatarModelPrefabs;

    //Scripts for PC
    public PCMoveProvider pCMoveProvider;
    public CameraMouseMovement cameraMouseMovement;
    
    //Camera
    public GameObject CameraOffset;

    //UI
    public TextMeshProUGUI PlayerNameText;

    void Update()
    {
        if(!CurrentPlatformManager.instance.IsOnQuest())
        {
            LocalXRRigGameobject.transform.rotation =  Quaternion.Euler(0f,CameraOffset.transform.eulerAngles.y,0f);
        }
    }

    // When the player is spawned
    void Start()
    {
        if(photonView.IsMine)
        {
            
            //Check if it's on PC or Quest
            if(!CurrentPlatformManager.instance.IsOnQuest()){

                //Remove VR Components
                LocalXRRigGameobject.GetComponent<AvatarInputConverter>().enabled = false;
                LocalXRRigGameobject.GetComponent<LocomotionSystem>().enabled = false;
                LocalXRRigGameobject.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;

                //Adjust Player Hands
                AvatarLeftHandGameObject.transform.position = new Vector3(-0.3f, 0.6f, 0.3f);
                AvatarRightHandGameObject.transform.position = new Vector3( 0.3f, 0.6f, 0.3f);

                //PC Components
                AvatarFullBody.transform.parent = LocalXRRigGameobject.transform;
                CameraOffset.transform.position = new Vector3(0f,1f,0f);     
                pCMoveProvider.enabled = true;       
                cameraMouseMovement.enabled = true;    
            }

            //If the player is local
            LocalXRRigGameobject.SetActive(true);

            //Loading correct avatar model
            object avatarSelectionNumber;
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                print("Avatar selection number:" + (int) avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int) avatarSelectionNumber);
            }

            //AvatarSelectionManager.Instance

            //Change their layers so the local player won't see their own body
            SetLayerRecursively(AvatarHeadGameObject, 6 );
            SetLayerRecursively(AvatarHeadGameObject, 7 );

            //Checks for Teleportation areas
            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if(teleportationAreas.Length > 0)
            {
                print("Found " + teleportationAreas.Length + " teleportation area.");
                foreach(var item in teleportationAreas)
                {
                    //Adds the teleportation area to the local XR Rig Provider
                    item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }
            
            MainAvatarGameObject.AddComponent<AudioListener>();

        }else
        {
            //The player is remote (disable XR Rig)
            LocalXRRigGameobject.SetActive(false);

            //Change their layers so the local player can see other people's bodies
            SetLayerRecursively(AvatarBodyGameObject, 0 );
            SetLayerRecursively(AvatarHeadGameObject, 0 );
        }

        if(PlayerNameText != null)
        {
            PlayerNameText.text = photonView.Owner.NickName;
        }
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    GameObject GetChildWithName(GameObject obj, string name) {
        Transform trans = obj.transform;
        Transform childTrans = trans. Find(name);
        if (childTrans != null) {
            return childTrans.gameObject;
        } else {
            return null;
        }
    }

    //PunRPC -> Is Executed for all players
    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber],LocalXRRigGameobject.transform);

        AvatarInputConverter avatarInputConverter = LocalXRRigGameobject.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
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
}
