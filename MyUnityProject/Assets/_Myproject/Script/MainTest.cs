using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Myshape
{
    public Transform Transform;
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;

    public Vector3 startPosition;
    public Mesh mesh;
    public Material material;
}


public class MainTest : MonoBehaviour
{
   public List<Myshape> shapes;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Myshape shape in shapes)
        {
            shape.MeshFilter.mesh = shape.mesh;
            shape.MeshRenderer.material = shape.material;
            shape.Transform.position = shape.startPosition;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
