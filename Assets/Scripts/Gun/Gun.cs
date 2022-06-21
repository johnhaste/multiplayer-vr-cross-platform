using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour, IPunObservable
{
    //Gun Properties
    public GameObject spawnPoint;

    //Projectile Properties
    public GameObject projectile;
    public float projectileSpeed = 100f;

    [PunRPC]
    public void ShootBullet()
    {
        GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity) as GameObject;

        //Plays the sound at that position
        AudioSource music = GetComponent<AudioSource>();
		music.Play();

        bullet.transform.rotation =  Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z);

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
