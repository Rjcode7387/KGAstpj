using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spaceshooter{
public class Bakcground : MonoBehaviour
{
    public float flowSpeed;
    void Update()
    {
        //transform : �� ������Ʈ�� ������ ������Ʈ�� Transform ������Ʈ
        //Trans.Translate :���� Position���� �Ķ������ Vector ����ŭ �̵�
        //Vector3.down : new Vector3(0,-1,0)
        transform.Translate(Vector3.down* Time.deltaTime*flowSpeed);
        if(transform.position.y < -2.55f)
            transform.position =Vector3.zero;
    }
}
}
