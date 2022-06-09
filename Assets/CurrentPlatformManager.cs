using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlatformManager : MonoBehaviour
{
    public static CurrentPlatformManager instance;
    public RuntimePlatform currentPlatform;
 
    //Singleton
    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets the current platform
        currentPlatform = Application.platform;
    }

}
