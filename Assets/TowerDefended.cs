using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TowerDefended : MonoBehaviour, IPunObservable
{
    public TextMeshProUGUI healthText;
    public int towerLives = 5;

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

    // Start is called before the first frame update
    void Start()
    {
        towerLives = 5;
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
        throw new System.NotImplementedException();
    }
}
