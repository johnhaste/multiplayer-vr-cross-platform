using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInteractionProvider : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weaponAttached;
 
    void Update()
    {

        if(Application.isFocused)
        {
            //MOUSE INPUT
            //Shoot Bullet
            if(Input.GetMouseButtonDown(0) && weaponAttached != null)
            {
                weaponAttached.GetComponent<Gun>().ShootBullet();
            }

            //MOVEMENT INPUT WASD OR ARROW KEYS
            Vector3 v3 = new Vector3(0f,0f,0f);
        
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {  
                v3 += Vector3.forward;  
            }  
            if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) {  
                v3 += Vector3.back;  
            }
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {  
                v3 += Vector3.left;  
            }    
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {    
                v3 += Vector3.right; 
            }  
            
            transform.Translate(speed * v3.normalized * Time.deltaTime);
        }      
    }

    public void AttachWeapon(GameObject weapon)
    {
        weaponAttached = weapon;
    }
}
