using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float rotatedSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotatedSpeed*Time.deltaTime);
    }
}
