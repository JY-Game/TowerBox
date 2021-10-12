using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;


    public void Spawn()
    {
        Vector3 tmp = transform.position;//z: -10 
        tmp.z = 0f;
        box.transform.position = tmp;

        Instantiate(box);
    }
   
}
