using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    public ParticleSystem fxBlood;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            fxBlood.Play();
            col.gameObject.GetComponent<Enemy>().LoseLives(1);
        }
    }

}
