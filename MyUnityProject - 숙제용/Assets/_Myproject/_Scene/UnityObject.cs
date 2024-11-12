using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityObject : MonoBehaviour
{
    public GameObject testObject;

    public Transform cubetransform;

    public MeshRenderer cubeMeshRenderer;

    public MeshFilter cubemeshFilter;

    public BoxCollider cubeboxCollider1;

    public BoxCollider cubeboxCollider2;

    public object systemObject;

    public Object unityObject;





    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"test of object value :{ testObject}");

        Debug.Log($"collider1.center.z : {cubeboxCollider1.center.z}");
        Debug.Log($"collider1.center.z : {cubeboxCollider2.center.z}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
