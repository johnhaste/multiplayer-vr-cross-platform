using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparePositions : MonoBehaviour
{
    public static ComparePositions instance;
 
    //Singleton
    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static bool IsClose(GameObject object1, GameObject object2, float distance)
    {
        if(Vector3.Distance(object1.transform.position,object2.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
