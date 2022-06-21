using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FieldOfAttack : MonoBehaviour
{
    public Enemy enemy;
    public GameObject zombieParent;

    void OnTriggerEnter(Collider col)
    {
        //print("Attack:" + col.name);
        if(col.name.Contains("Avatar"))
        {
           enemy.AttackPlayer(col.gameObject);
        }
      
        if(col.name == "FirePlace")
        {
            zombieParent.GetComponent<PhotonView>().RPC("BurnEnemy", RpcTarget.AllBufferedViaServer);
            col.GetComponent<PhotonView>().RPC("LoseLive", RpcTarget.AllBufferedViaServer);
        }
    }
}
