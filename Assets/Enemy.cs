using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Enemy : MonoBehaviour, IPunObservable
{
    public Animator animator;
    public GameObject fieldOfView;
    private bool isWalking;

    //Health
    public int health;
    public TextMeshProUGUI healthText;

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
        health = 5;     
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            animator.SetBool("isWalking", isWalking);
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.name == "FirePlace")
        {
            Die();
        }
    }

    [PunRPC]
    public void LoseHealth(int damage)
    {

        health -= damage;

        if(health > 0)
        {    
            UpdateHealthUI();
        }
        else
        {
            GetComponent<FollowObject>().enabled = false;
            DestroyHealthUI();
            Die();
        }
        
    }

    public void UpdateHealthUI()
    {
        healthText.text = health+"";
    }

    public void DestroyHealthUI()
    {
        Destroy(healthText);
    }

    public void Die()
    {
        health = 0;
        animator.SetTrigger("die");
        StartCoroutine("WaitAndDie"); 
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!stream.IsWriting)
        {
            health = (int) stream.ReceiveNext(); 
        }
    }
}
