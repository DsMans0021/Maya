using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikesCTRL : MonoBehaviour
{
    float wait = 0.2f;
    public GameObject FallingSpikes;
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
        Instantiate(FallingSpikes, new Vector3(Random.Range(-10f, 20f), 10, 0), Quaternion.identity);
    }
}
