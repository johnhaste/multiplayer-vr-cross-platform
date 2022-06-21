using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    public ParticleSystem fxBlood;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            fxBlood.transform.parent = null;
            fxBlood.Play();

            col.gameObject.GetComponent<PhotonView>().RPC("LoseHealth", RpcTarget.AllBufferedViaServer, 1);
            Destroy(gameObject);
        }
    }

}
