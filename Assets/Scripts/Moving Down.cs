using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDown : MonoBehaviour
{
    private float speed = -2f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0, speed);
    }
}
