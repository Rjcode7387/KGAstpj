using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform followTarget;
    public float moveSpeed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime*moveSpeed);
        //0.02*3 = 0.05~0.06
    }
}
