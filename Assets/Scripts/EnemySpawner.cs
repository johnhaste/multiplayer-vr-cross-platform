using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour, IPunObservable
{
    public GameObject[] spawnerPoint;
    public GameObject zombiePrefab;
    public ParticleSystem fxSpawn;
    public float timeRate = 5f;
    public int enemyCounter;

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
            gameObject.GetComponent<PhotonView>().RPC("SpawnZombie", RpcTarget.AllBufferedViaServer);
            yield return new WaitForSeconds(timeRate);
        }
    }

    [PunRPC]
    public void SpawnZombie()
    {
        int indexCurrentSpawner = Random.Range(0,spawnerPoint.Length);
        spawnerPoint[indexCurrentSpawner].GetComponent<InitialParticleEmission>().PlayChildFX();
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
