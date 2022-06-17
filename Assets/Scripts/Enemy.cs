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
            //ScoreManager.instance.GetComponent<PhotonView>().RPC("AddScore", RpcTarget.AllBufferedViaServer,50);
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
        Attack();
        StartCoroutine("WaitAndLookForPlayer", player);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
        isWalking = false;
        GameObject player = null;
        StartCoroutine("WaitAndWalk", player); 
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator WaitAndLookForPlayer(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        
        //se estiver próximo do player ataca de novo
        if(ComparePositions.IsClose(gameObject, player, 1f))
        {
            AttackPlayer(player);
        }
        else
        {
            isWalking = true;
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
