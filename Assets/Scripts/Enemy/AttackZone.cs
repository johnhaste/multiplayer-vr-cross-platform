using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttackZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        print("Attack Detector:" + col.name);
        if(col.name.Contains("Avatar"))
        {
            col.GetComponentInParent<PhotonView>().RPC("LoseHealth", RpcTarget.AllBufferedViaServer,1);
        }
    }
}
