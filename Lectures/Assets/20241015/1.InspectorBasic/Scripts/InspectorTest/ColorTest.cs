using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    public Color targetColor;

    public Renderer targetRenderer;

    // Start is called before the first frame update
    void Start()
    {
        targetRenderer.material.color = targetColor;
    }
    
}
