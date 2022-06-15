using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class DebugHealthPhoton : MonoBehaviour, IPunObservable
{
    public TextMeshProUGUI healthText;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<PhotonView>().RPC("LoseHealth", RpcTarget.AllBufferedViaServer, 1);
        }
    }

    public void UpdateHealthUI()
    {
        healthText.text = health+"";
    }

    [PunRPC]
    public void LoseHealth(int damage)
    {
        health -= damage;
        UpdateHealthUI();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!stream.IsWriting)
        {
            health = (int) stream.ReceiveNext(); 
        }
    }
}
