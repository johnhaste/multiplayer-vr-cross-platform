using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpherePlayerNetworkSetup : MonoBehaviourPunCallbacks, IPunObservable
{
    //XR Rig
    public GameObject LocalXRRigGameobject;

    //Avatar Body Parts
    public GameObject MainAvatarGameObject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
    public GameObject AvatarLeftHandGameObject;
    public GameObject AvatarRightHandGameObject;
    public GameObject AvatarInputConverter;
    public GameObject[] AvatarModelPrefabs;

    //Health
    public int health;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public GameObject canvasRedBlink;

    //Restart Black UI
    public GameObject canvasBlackRestart;
    public TextMeshProUGUI restartCountdownText;

    //Grab Detector
    public GameObject grabDetector;

    //Scripts for PC
    public GameObject mainCamera;
    public Camera playerCamera;
    public PCInteractionProvider pCMoveProvider;
    public CameraMouseMovement cameraMouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        canvasRedBlink.gameObject.SetActive(false);

        if(photonView.IsMine)
        {
            //Check if it's on PC or Quest
            if(!CurrentPlatformManager.instance.IsOnQuest())
            {
                //Remove VR Components
                LocalXRRigGameobject.SetActive(false);

                //Adjust Player Hands and Body
                AvatarLeftHandGameObject.transform.position  = new Vector3(-0.2f, 1.3f, 0.3f);
                AvatarRightHandGameObject.transform.position = new Vector3( 0.2f, 1.3f, 0.3f);
                AvatarHeadGameObject.transform.position      = new Vector3( 0.0f, 1.5f, 0.0f);
                AvatarBodyGameObject.transform.position      = new Vector3( 0.0f, 0.8f, 0.0f);

                AvatarLeftHandGameObject.transform.SetParent(mainCamera.transform);
                AvatarRightHandGameObject.transform.SetParent(mainCamera.transform);

                //PC Components
                //AvatarFullBody.transform.parent = LocalXRRigGameobject.transform;
                //CameraOffset.transform.position = new Vector3(0f,1f,0f);     
                playerCamera.enabled = true;
                pCMoveProvider.enabled = true;
                cameraMouseMovement.enabled = true;
            }
            else //On quest
            {
                //VR Components
                //GetComponent<SphereCollider>().enabled = false;
                LocalXRRigGameobject.SetActive(true);
                grabDetector.transform.SetParent(LocalXRRigGameobject.transform);

                //PC Components
                //AvatarFullBody.transform.parent = LocalXRRigGameobject.transform;
                //CameraOffset.transform.position = new Vector3(0f,1f,0f);     
                playerCamera.enabled = false;
                pCMoveProvider.enabled = false;
                cameraMouseMovement.enabled = false;
            }           

            //Loading correct avatar model
            object avatarSelectionNumber;
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                print("Avatar selection number:" + (int) avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int) avatarSelectionNumber);
            }

            gameObject.AddComponent<AudioListener>();
        }
        else //Not your player
        {  
            
            //Check if it's on PC or Quest
            if(CurrentPlatformManager.instance.IsOnQuest())
            {
                //grabDetector.transform.SetParent(MainAvatarGameObject.transform);
            }
            else
            {
                LocalXRRigGameobject.SetActive(false);
                AvatarLeftHandGameObject.transform.position  = AvatarBodyGameObject.transform.position + new Vector3(-1f,0f,0f);
                AvatarRightHandGameObject.transform.position = AvatarBodyGameObject.transform.position + new Vector3(1f,0f,0f);;
            }

            //Adjust Player Hands and Body
            //AvatarHeadGameObject.transform.position      = new Vector3( 0.0f, 1.5f, 0.0f);
            //AvatarBodyGameObject.transform.position      = new Vector3( 0.0f, 0.8f, 0.0f);
            //AvatarLeftHandGameObject.transform.position  = AvatarBodyGameObject.transform.position;
            //AvatarRightHandGameObject.transform.position = AvatarBodyGameObject.transform.position;

            AvatarLeftHandGameObject.transform.SetParent(mainCamera.transform);
            AvatarRightHandGameObject.transform.SetParent(mainCamera.transform);   

            //Change their layers so the local player can see other people's bodies
            SetLayerRecursively(gameObject, 12 );
            SetLayerRecursively(canvasRedBlink, 14 );

            playerCamera.enabled = false;
            pCMoveProvider.enabled = false;
            cameraMouseMovement.enabled = false;
        }
    }

    [PunRPC]
    public void LoseHealth(int damage)
    {
        if(!canvasRedBlink.gameObject.active)
        {
            StartCoroutine("BlinkRedCanvas");

            if(health > 0)
            {    
                health -= damage;
                UpdateHealthUI();
            }
            else
            {
                DestroyHealthUI();
                Die();
            }
        }
    }

    IEnumerator BlinkRedCanvas()
    {
        canvasRedBlink.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvasRedBlink.gameObject.SetActive(false);
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

        if(!CurrentPlatformManager.instance.IsOnQuest())
        {
            print("Not on quest");
            AvatarInputConverter.SetActive(false);
        }else{
            print("On quest");
            AvatarInputConverter.SetActive(true);
        }

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

    public void UpdateHealthUI()
    {
        healthBar.rectTransform.sizeDelta = new Vector2( (float) health/10, 0.1f); 
        healthText.text = health+"";
    }

    public void DestroyHealthUI()
    {
        Destroy(healthBar);
    }

    public void Die()
    {
        health = 0;
        MainAvatarGameObject.transform.rotation =  Quaternion.Euler(90f,0f,90f);
        LocalXRRigGameobject.SetActive(false);
        pCMoveProvider.enabled = false;
        StartCoroutine("WaitAndDie"); 
    }

    IEnumerator WaitAndDie()
    {
        //Disable Movement
        pCMoveProvider.enabled = false;
        cameraMouseMovement.enabled = false;
        LocalXRRigGameobject.SetActive(false);

        //Canvas
        canvasBlackRestart.SetActive(true);
        restartCountdownText.text = 3+"";
        yield return new WaitForSeconds(1f);
        restartCountdownText.text = 2+"";
        yield return new WaitForSeconds(1f);
        restartCountdownText.text = 1+"";
        yield return new WaitForSeconds(1f);
        PhotonNetwork.LoadLevel("HomeScene");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
