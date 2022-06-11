using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetPosition;
    public GameObject  player;
    public float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = player.GetComponent<Transform>();

        transform.LookAt(targetPosition);
     
        if(Vector3.Distance(transform.position,targetPosition.position) >= 1f){
        
            transform.position += transform.forward*speed*Time.deltaTime;
        }
        
        /*    
        if(Vector3.Distance(transform.position,Player.position) <= MaxDist)
        {
            //Here Call any function U want Like Shoot at here or something
        } 
        */
    }
}
