using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfAttack : MonoBehaviour
{
    public Enemy enemy;

    void OnTriggerEnter(Collider col)
    {
        //print("Attack:" + col.name);
        if(col.name.Contains("Player"))
        {
           enemy.AttackPlayer(col.gameObject);
        }
    }
}
