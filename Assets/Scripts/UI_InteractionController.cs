using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField]
    GameObject UIController;

    [SerializeField]
    GameObject BaseController;

    [SerializeField]
    InputActionReference inputActionReference_UISwitcher;

    bool isUICanvasActive = false;

    [SerializeField]
    GameObject UIGameobjects;

    [SerializeField]
    Vector3 positionOffsetForUICanvasGameobject;

    private void OnEnable()
    {
        inputActionReference_UISwitcher.action.performed += ActivateUIMode;
    }
    private void OnDisable()
    {
        inputActionReference_UISwitcher.action.performed -= ActivateUIMode;
    }

    private void Start()
    {
        
        //Get current scene
        Scene scene = SceneManager.GetActiveScene();
        
        //Home Scene
        if (scene.name == "HomeScene")
        {
            //Activating UI Controller 
            UIGameobjects.SetActive(true);
            UIController.GetComponent<XRRayInteractor>().enabled = true;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = true;
        }
        else //In Game
        {
            //Deactivating UI Controller 
            UIGameobjects.SetActive(false);
            UIController.GetComponent<XRRayInteractor>().enabled = false;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = false;
        }  

        
    }

    /// <summary>
    /// This method is called when the player presses UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    /// <param name="obj"></param>
    private void ActivateUIMode(InputAction.CallbackContext obj)
    {
        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = true;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

            //Deactivating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = false;

            //Adjusting the transform of the UI Canvas Gameobject according to the VR Player transform
            Vector3 positionVec = new Vector3(UIController.transform.position.x, positionOffsetForUICanvasGameobject.y, UIController.transform.position.z);
            Vector3 directionVec = UIController.transform.forward;
            directionVec.y = 0f;
            UIGameobjects.transform.position = positionVec + positionOffsetForUICanvasGameobject.magnitude * directionVec;
            UIGameobjects.transform.rotation = Quaternion.LookRotation(directionVec);

            //Activating the UI Canvas Gameobject
            UIGameobjects.SetActive(true);
        }
        else
        {
            isUICanvasActive = false;

            //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = false;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

            //Activating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = true;

            //De-Activating the UI Canvas Gameobject
            UIGameobjects.SetActive(false);
        }

    }

    private void ActivateUIMode()
    {
        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = true;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

            //Deactivating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = false;

            //Adjusting the transform of the UI Canvas Gameobject according to the VR Player transform
            Vector3 positionVec = new Vector3(UIController.transform.position.x, positionOffsetForUICanvasGameobject.y, UIController.transform.position.z);
            Vector3 directionVec = UIController.transform.forward;
            directionVec.y = 0f;
            UIGameobjects.transform.position = positionVec + positionOffsetForUICanvasGameobject.magnitude * directionVec;
            UIGameobjects.transform.rotation = Quaternion.LookRotation(directionVec);

            //Activating the UI Canvas Gameobject
            UIGameobjects.SetActive(true);
        }
        else
        {
            isUICanvasActive = false;

            //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = false;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

            //Activating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = true;

            //De-Activating the UI Canvas Gameobject
            UIGameobjects.SetActive(false);
        }

    }
}
