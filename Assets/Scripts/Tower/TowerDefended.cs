using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TowerDefended : MonoBehaviour, IPunObservable
{
    public TextMeshProUGUI healthText;
    private int towerLives = 10;
    public GameObject fireParticleGameObject;

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
        if(towerLives > 1)
        {
            towerLives--;
            UpdateLivesGUI();
            UpdateFireSize();
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
        UpdateFireSize();
    }

    void UpdateFireSize()
    {
        fireParticleGameObject.transform.localScale = new Vector3((float) towerLives/10, (float) towerLives/10,(float) towerLives/10);
    }

    void Die()
    {
       healthText.text = "Score:" + ScoreManager.instance.score; 
       GameStateManager.instance.currentGameState = GameStateManager.gameState.ENDGAME;
       //StartCoroutine("WaitAndRestartGame");
    }

    IEnumerator WaitAndRestartGame()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("HomeScene");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
