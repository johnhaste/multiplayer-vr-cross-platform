using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialParticleEmission : MonoBehaviour
{
    public ParticleSystem fxSpawn;

    public void PlayChildFX()
    {
        fxSpawn.Play();
    }

}
