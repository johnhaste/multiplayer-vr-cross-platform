using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlatformManager : MonoBehaviour
{
    public static CurrentPlatformManager instance;
    public RuntimePlatform currentPlatform;

    private bool simulateOnQuest = false;

    //Singleton
    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        
        //Gets the current platform
        currentPlatform = Application.platform;
        print("CurrentPlatform:" + currentPlatform);

        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets the current platform
        //currentPlatform = Application.platform;
        //print("CurrentPlatform:" + currentPlatform);
        print("Is on quest?" + IsOnQuest());
    }

    public static RuntimePlatform GetCurrentPlatform()
    {
        return Application.platform;
    }

    public bool IsOnQuest()
    {
        if(simulateOnQuest){return true;}
            
        if(GetCurrentPlatform() == RuntimePlatform.WindowsEditor || GetCurrentPlatform() == RuntimePlatform.WindowsPlayer)
        {
            return false;
        }else
        {
            return true;
        }
    }

}
