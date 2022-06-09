using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformInteractionManager : MonoBehaviour
{
    private RuntimePlatform currentPlatform;
    
    public GameObject XROrigin;
    public GameObject UI;

    public bool simulateOnQuest;
    
    // Start is called before the first frame update
    void Start()
    {
       

        if(CurrentPlatformManager.instance == null)
        {
            gameObject.AddComponent<CurrentPlatformManager>();
        }

        print("Is on quest?" + CurrentPlatformManager.instance.IsOnQuest());

        Scene scene = SceneManager.GetActiveScene();
        
        if((!CurrentPlatformManager.instance.IsOnQuest()))
        {
            //Home Scene
            if (scene.name == "HomeScene")
            {
                XROrigin.transform.position = new Vector3(0,1,-1);
                XROrigin.transform.rotation = Quaternion.Euler(0f,15f,0f);
                UI.SetActive(true);
            }
            else //In Game
            {

            }   

        }
        else //Is on Quest
        {
            if (scene.name == "HomeScene")
            {
                UI.SetActive(false);
            }
            else //In Game
            {

            }  
        }
    }

}
