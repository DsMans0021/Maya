using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    private Transform cloudTransform;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cloudTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        cloudTransform.position += Vector3.right * speed * Time.deltaTime;
        rb.velocity = new Vector2(speed, 0);
        speed += 0.00005f;
        cloudTransform.position = new Vector3(cloudTransform.position.x, cameraTransform.position.y, cloudTransform.position.z);
    }
}
