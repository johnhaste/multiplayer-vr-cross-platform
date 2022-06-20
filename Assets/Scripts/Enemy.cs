using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Enemy : MonoBehaviour, IPunObservable
{
    public Animator animator;
    public GameObject fieldOfView;
    public bool isWalking;
    public bool isAttacking;

    //Main Turret Target
    public GameObject mainTarget;

    //Health
    public int health;
    public TextMeshProUGUI healthText;
    public Image healthBar;

    public GameObject enemySpawner;

    //Photon
    public PhotonView m_photonView;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Components
        enemySpawner = GameObject.Find("EnemySpawner");
        animator = GetComponent<Animator>();

        //Spawner
        enemySpawner.GetComponent<EnemySpawner>().AddOneEnemyCounter();
        m_photonView.ViewID = 100 + enemySpawner.GetComponent<EnemySpawner>().enemyCounter;
        
        //Base Values
        isWalking = true;  

        //Health 
        health = 5;
        UpdateHealthUI();  
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            animator.SetBool("isWalking", isWalking);
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
            ScoreManager.instance.GetComponent<PhotonView>().RPC("AddScore", RpcTarget.AllBufferedViaServer,50);
            GetComponent<FollowObject>().enabled = false;
            DestroyHealthUI();
            Die();
        }
        
    }

    [PunRPC]
    public void BurnEnemy()
    {
        health = 0;
        GetComponent<FollowObject>().enabled = false;
        DestroyHealthUI();
        Die();
    }
    
    public void UpdateHealthUI()
    {
        healthText.text = health+"";
        healthBar.rectTransform.sizeDelta = new Vector2( (float) health/10, 0.1f); 
    }

    public void DestroyHealthUI()
    {
        Destroy(healthText);
        Destroy(healthBar);
    }

    public void Die()
    {
        health = 0;
        animator.SetTrigger("die");
        StartCoroutine("WaitAndDie"); 
    }

    public void AttackPlayer(GameObject player)
    {
        if(!isAttacking)
        {
            isAttacking = true;
            isWalking = false;
            animator.SetTrigger("attack");
        }
        
        StartCoroutine("WaitAndLookForPlayer", player);
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator WaitAndLookForPlayer(GameObject player)
    {
        yield return new WaitForSeconds(2f);

        //If it's close to the player, attacks again
        if(ComparePositions.IsClose(gameObject, player, 3f))
        {
            isAttacking = false;
            AttackPlayer(player);
        }
        else //if it's far from the player, gets back to its route
        {
            isAttacking = false;
            isWalking = true;
            GetComponent<FollowObject>().ChangeTarget(GameObject.Find("InitialTarget"));
        }
    }

    IEnumerator WaitAndWalk()
    {
        yield return new WaitForSeconds(1f);
        isWalking = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
