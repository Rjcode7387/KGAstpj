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
        //0.02(deltaTime) * 3(movespeed) : �Ÿ��� ���� ������ ���� �����̱⿡ target�� �����ǿ� ����������� �������� ȿ���� ������ �ִ�.
    }
}
