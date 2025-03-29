using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsPoints : MonoBehaviour
{
    public int stars = 0;
    public Text starsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       starsText.text = "StartCount: "+ stars.ToString(); 
    }
}
