using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public enum gameState{
        PREGAME, INGAME, ENDGAME
    }

    public static GameStateManager instance;
    public gameState currentGameState;
 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
       currentGameState = gameState.INGAME; 
    }

}
