using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        print("Shoot");
        if(weaponAttached != null)
        {
            weaponAttached.gameObject.transform.GetChild(0).GetComponent<Gun>().ShootBullet();
        }

    }

    public void AttachWeapon(GameObject weapon)
    {
        weaponAttached = weapon;
    }

}
