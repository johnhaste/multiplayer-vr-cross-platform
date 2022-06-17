using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class CustomBaseControllerManager : MonoBehaviour
{
    [SerializeField]
    InputActionReference inputActionReference_Shoot;

    public GameObject weaponAttached;

    void Start()
    {
        inputActionReference_Shoot.action.performed += ShootCurrentGun;
    }

    private void ShootCurrentGun(InputAction.CallbackContext obj)
    {
        if(weaponAttached != null)
        {
           weaponAttached.GetComponent<PhotonView>().RPC("ShootBullet", RpcTarget.AllBufferedViaServer);
        }

    }

    public void AttachWeapon(GameObject weapon)
    {
        weaponAttached = weapon;
    }

}
