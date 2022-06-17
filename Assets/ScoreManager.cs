using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviour, IPunObservable
{

    public int score;

    public static ScoreManager instance;
    private void Awake()
    {
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
        score = 0;
    }

    [PunRPC]
    public void AddScore(int score)
    {
        this.score += score;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
