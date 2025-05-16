using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsPoints : MonoBehaviour
{
    public int stars = 0;
    public Image starsImage;           // Reference to the UI Image
    public Sprite[] starSprites;      // Array of 6 sprites (index 0 = 0 stars, up to index 5)

    void Start()
    {
        UpdateStarImage();
    }

    void Update()
    {
        // You can remove this if stars only change when something happens (e.g. after collecting a star)
        UpdateStarImage();
    }

    void UpdateStarImage()
    {
        // Clamp to ensure the stars value is within range
        stars = Mathf.Clamp(stars, 0, 5);

        // Change the UI image sprite based on star count
        if (starSprites.Length > stars)
        {
            starsImage.sprite = starSprites[stars];
        }
    }
}