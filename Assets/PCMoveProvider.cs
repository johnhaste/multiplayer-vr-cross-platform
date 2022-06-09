using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMoveProvider : MonoBehaviour
{
    public float speed = 3;
 
    void Update()
    {
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
