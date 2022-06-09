using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class UIInteractionControllerSwitcher : MonoBehaviour
{
    [SerializeField]
    GameObject UIController;

    [SerializeField]
    GameObject BaseController;

    public void DisableGrab()
    {
        ActivateUIMode();
    }

    public void EnableGrab()
    {
        DeactivateUIMode();
    }   

    private void ActivateUIMode()
    {
        //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
        UIController.GetComponent<XRRayInteractor>().enabled = true;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

        //Deactivating Base Controller by disabling its XR Direct Interactor
        BaseController.GetComponent<XRDirectInteractor>().enabled = false;
    }


    private void DeactivateUIMode()
    {
        //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
        UIController.GetComponent<XRRayInteractor>().enabled = false;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

        //Activating Base Controller by disabling its XR Direct Interactor
        BaseController.GetComponent<XRDirectInteractor>().enabled = true;
    }
}
