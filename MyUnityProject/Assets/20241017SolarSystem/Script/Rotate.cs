using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float selfmovespeed;

    void Update()
    {
        Selfspin();
    }


    // Update is called once per frame
    void Selfspin()
    {
        transform.Rotate(Vector3.up, selfmovespeed*Time.deltaTime);
    }
}
