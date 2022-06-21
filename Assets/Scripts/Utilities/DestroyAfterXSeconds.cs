using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{

    public int seconds;

    void Start()
    {
        StartCoroutine("WaitAndDestroy", seconds); 
    }

   IEnumerator WaitAndDestroy(int seconds)
   {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
   }

}
