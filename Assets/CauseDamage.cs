using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        print("Hitted:"+ col.name);
        if(col.gameObject.tag == "Enemy")
        {
            print("Enemy hit");
            col.gameObject.GetComponent<Enemy>().LoseLives(1);
        }
    }
}
