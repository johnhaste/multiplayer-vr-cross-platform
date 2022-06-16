using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnSpherePlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject Sphere;

    public Vector3 spawnPosition = new Vector3(0f,0f,0f);

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(Sphere.name, spawnPosition, Quaternion.identity );
        }
    }
}
