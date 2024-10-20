using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSmooth : MonoBehaviour
{
    public Transform followTarget;
    public float moveSpeed;

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, 
            Time.deltaTime * moveSpeed);
        //0.02(deltaTime) * 3(movespeed) : 거리는 가까운데 비율인 값은 고정이기에 target에 포지션에 가까워질수록 느려지는 효과를 느낄수 있다.
    }
}
