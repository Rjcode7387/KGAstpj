using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoTest : MonoBehaviour
{
    public Color targetColor;

    public Renderer targetRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }
        

    // Update is called once per frame
    void Update()
    {
            targetRenderer.material.color = targetColor;
        
    }
}
