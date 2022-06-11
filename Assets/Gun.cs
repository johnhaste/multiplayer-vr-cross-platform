using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun Properties
    public GameObject spawnPoint;

    //Projectile Properties
    public GameObject projectile;
    public float projectileSpeed = 200f;

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        bullet.transform.rotation =  Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }
}
