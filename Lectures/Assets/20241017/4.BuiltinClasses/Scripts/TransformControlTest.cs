using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControlTest : MonoBehaviour
{
    public Transform target;
    //기본적으로 Transform의 position, rotation, localscale 프로퍼티를 통해
    //해당 오브젝트의 위치 , 각도 , 크기 등을 제어할수 있다.
    void Start()
    {
        //ControlByValue();
        //ControlByDirection();
        //ControlByMethod();
    }

    void Update()
    {
        //transform.position = transform.position + new Vector3(3,2,1) * Time.deltaTime;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(30,20,10) * Time.deltaTime);

        transform.Translate(new Vector3(3,2,1)* Time.deltaTime);
        transform.Rotate(new Vector3(30, 20, 10) * Time.deltaTime);
    }

    //값을 직접 대입하여 Transform을 제어
    private void ControlByValue() 
    {
        transform.position = new Vector3(3, 2, 1);
        //transform.rotation = new Quaternion(0.3f,0.02f,0.001f,0f);
        transform.rotation = Quaternion.Euler(30, 20, 10);
        transform.localScale = new Vector3(4, 5, 6);
    }

    //방향(x : 좌,우 y : 상,하 z :전,후)에 방향 벡터를 대입하여 Rotation을 제어
    private void ControlByDirection() 
    {
        //방향벡터를 구하여 up에 대입 
        Vector3 newDirection = target.position - transform.position;
        //내 위치에서 Target 위치로 이동하기 위해 향해야하는 방향 벡터를 구함
        //transform.up = target.position - transform.position; 해당 방향이 나의 up이 됨

        //해당 방향의 나의 right이 되려면
        //transform.right = newDirection;

        //해당 방향이 나의 forward가 되려면
        //transform.forward = newDirection;

        //해당 방향이 나의 down이 되려면
        //transform.up = transform.up - newDirection;

        //해당 방향이 나의 backward가 되려면
        //transform.forward = transform.forward - newDirection;

        //해당 방향이 나의 left가 되려면;
        //transform.right = transform.right - newDirection;    
    }

    //Transform의 제어 함수를 호출
    private void ControlByMethod() 
    {
        //Translate : Position을 제어하는 함수
        transform.Translate(5,0,0);
        //rotate : rotation을 제어하는 함수
        transform.Rotate(30,0,0);
        //Translate , Rotate 함수를 사용하여 제어하는 것은
        //transform.position, rotation에 값을 직접 할당하는 것에 비교하자면
        //현재 position, rotation을 기준으로 상대적인 상대적인 연산이 이루어지는것으로 이해하면 된다.
    }

    
}
