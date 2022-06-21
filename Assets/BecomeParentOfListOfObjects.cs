using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeParentOfListOfObjects : MonoBehaviour
{

    public List<GameObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objects)
        {
            obj.transform.SetParent(gameObject.transform);
        }
    }
}
