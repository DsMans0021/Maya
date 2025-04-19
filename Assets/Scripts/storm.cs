using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class storm : MonoBehaviour
{
    float wait = 1.5f;
    public GameObject FallingSpikes;
    public GameObject Allert;   
    private GameObject spawned; // Reference to the spawned object 
    
// Reference to the spawned object
    public float destroyDelay = 0.1f; // Time before the spawned object is destroyed

    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), wait, wait);
    }
    void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
   
    void Spawn()
    {
        GameObject spawnedObject = Instantiate(Allert, new Vector3(UnityEngine.Random.Range(-7f, 18f), 1.4f, 0), Quaternion.identity);
        spawned = spawnedObject; // Store the reference to the spawned object
        Invoke("Spawn2", 0.5f);
        Destroy(spawnedObject, destroyDelay); // Destroy the spawned object after the specified delay
    }
    void Spawn2()
    {
        GameObject spawnedObject2 = Instantiate(FallingSpikes, spawned.transform.position, Quaternion.identity);
        Destroy(spawnedObject2, destroyDelay); 
    }
}
