using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject fieldOfView;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", isWalking);
    }

}
