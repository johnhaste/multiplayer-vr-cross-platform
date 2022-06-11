using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject fieldOfView;
    private bool isWalking;

    public GameObject enemySpawner;

    //Photon
    public PhotonView m_photonView;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner");

        enemySpawner.GetComponent<EnemySpawner>().AddOneEnemyCounter();
        m_photonView.ViewID = 100 + enemySpawner.GetComponent<EnemySpawner>().enemyCounter;
        animator = GetComponent<Animator>();
        isWalking = true;        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", isWalking);
    }

}
