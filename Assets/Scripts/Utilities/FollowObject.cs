using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Transform targetPosition;
    public GameObject targetObject;
    public float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("InitialTarget");
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject != null)
        {
            targetPosition = targetObject.GetComponent<Transform>();
     
            var lookPos = targetPosition.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);

            if(Vector3.Distance(transform.position,targetPosition.position) >= 1f)
            {
                transform.position += transform.forward*speed*Time.deltaTime;   
            }
    
            /*
            if(Vector3.Distance(transform.position,targetPosition.position) < 1f)
            {
                GetComponent<Enemy>().Attack();
            } 
            */
        }
        
    }

    public void ChangeTarget(GameObject newTarget)
    {
        targetObject = newTarget;
    }
}
