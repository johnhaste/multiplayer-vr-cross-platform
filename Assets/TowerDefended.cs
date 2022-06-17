using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TowerDefended : MonoBehaviour, IPunObservable
{
    public TextMeshProUGUI healthText;
    private int towerLives = 20;

    public static TowerDefended instance;
    private void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GetComponent<PhotonView>().RPC("UpdateLivesGUI", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    public void LoseLive()
    {
        if(towerLives > 0)
        {
            towerLives--;
            UpdateLivesGUI();
        }
        else
        {
            Die();
        }
    }

    [PunRPC]
    void UpdateLivesGUI()
    {
        healthText.text = towerLives+"";
    }

    void Die()
    {
       healthText.text = "You Lost!"; 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
