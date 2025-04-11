using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopElevator : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float height = 11.7f;

    private SpriteRenderer spriteRenderer;
    private Vector2 startSize;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y + speed * Time.deltaTime);

        if (spriteRenderer.size.y > height)
        {
            spriteRenderer.size = startSize;
        }
    }
}
