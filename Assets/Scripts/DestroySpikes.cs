using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroySpikes : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
