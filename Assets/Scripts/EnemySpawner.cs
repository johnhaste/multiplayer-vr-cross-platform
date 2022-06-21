using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour, IPunObservable
{
    public GameObject[] spawnerPoint;
    public GameObject zombiePrefab;
    public float timeRate = 5f;
    public int enemyCounter;

    public ParticleSystem fxSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        StartCoroutine(CreateZombies());
    }

    private IEnumerator CreateZombies()
    {
        while (true)
        {
            int indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
            gameObject.GetComponent<PhotonView>().RPC("SpawnZombie", RpcTarget.AllBufferedViaServer, indexCurrentSpawner);
            yield return new WaitForSeconds(timeRate);
        }
    }

    [PunRPC]
    public void SpawnZombie(int indexCurrentSpawner)
    {
        ParticleSystem fxSpawning = Instantiate(fxSpawn, spawnerPoint[indexCurrentSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as ParticleSystem;      
        GameObject zombie = Instantiate(zombiePrefab, spawnerPoint[indexCurrentSpawner].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;      
    }

    public void AddOneEnemyCounter()
    {
        enemyCounter++;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f,1f,1f));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
