using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spaceshooter{
public class Bakcground : MonoBehaviour
{
    public float flowSpeed;
    void Update()
    {
        //transform : 이 컴포넌트가 부착된 오브젝트의 Transform 컴포넌트
        //Trans.Translate :현재 Position에서 파라미터의 Vector 값만큼 이동
        //Vector3.down : new Vector3(0,-1,0)
        transform.Translate(Vector3.down* Time.deltaTime*flowSpeed);
        if(transform.position.y < -2.55f)
            transform.position =Vector3.zero;
    }
}
}
