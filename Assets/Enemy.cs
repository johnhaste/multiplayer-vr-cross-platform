using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject fieldOfView;
    private bool isWalking;
    public int lives;

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
        lives = 5;     
    }

    // Update is called once per frame
    void Update()
    {
        if(lives > 0)
        {
            animator.SetBool("isWalking", isWalking);
        }
        
    }

    public void LoseLives(int damage)
    {
        if(lives > 0)
        {
            lives -= damage;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("die");
        StartCoroutine("WaitAndDie"); 
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
