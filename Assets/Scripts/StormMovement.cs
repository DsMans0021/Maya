using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    private Transform Move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Move = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Move the storm to the right
        Move.position += Vector3.right * speed * Time.deltaTime;
        rb.velocity = new Vector2(speed, 0);
        speed += 0.00005f;
    }
}
