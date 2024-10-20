using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControl : MonoBehaviour
{
    public Transform target;

    //기본적으로 Transform의 position, rotation,localScale 프로퍼티를 통해
    //해당 오브젝트의 위치,각도 , 크기등을 제어 할 수 있다.

    private void Start()
    {
        //ControlDirect();
        //ControlByDirection();
        ControlByMethod();

    }

    //값을 직접 대입하여 Transform을 제어
    private void ControlDirect()
    {
        transform.position = new Vector3(3, 2, 1);
        //transform.rotation = new Quaternion(0.3f, 0.02f, 0.001f, 0);
        transform.rotation = Quaternion.Euler(30, 20, 10);
        print(transform.rotation);
        transform.localScale = new Vector3(4, 5, 6);

    }


    //방향(상,하,좌,우,전,후) x축=상하, y축 =좌우, z축 = 전후
    private void ControlByDirection()
    {

        Vector3 lookdir = target.position - transform.position; 
        //내 위치에서 Target 위치로 이동하기위해 향해야 하는 방향 백터를 구함

        //transform.up = target.position - transform.position;

        //해당 방향이 나의 right가 되려면?
        //transform.right = lookdir;
        //해당 방향이 나의 forward가 되려면?
        //transform.forward = lookdir;



        // 오늘 해볼것
        //해당 방향이 나의 down이 되려면?
      
        //해당 방향이 나의 left가 되려면?
        
        //해당 방향이 나의 backward가 되려면?

    }


    //Transform의 제어 함수를 호출 
    private void ControlByMethod()
    {
        //Translate : Position을 제어하는 함수
        transform.Translate(5,0,0);
        //Rotate : Rotation을 제어하는 함수
        transform.Rotate(30,0,0);

        //Translate, Rotate 함수를 사용하여 제어하는 것은
        //transform.position,rotaion에 값을 직접 할당하는 것과 비교 하자면
        //현재 position, rotation 기준으로 상대적인 연산이 이루어지는 것으로 이해하면 된다.

    }



    //public Vector3 lookDir;
    
    private void Update()
    {
        //transform.position = transform.position+ new Vector3(3, 2, 1)*Time.deltaTime;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles+ new Vector3(30,20,10)*Time.deltaTime);

        transform.Translate(new Vector3(3, 2, 1)*Time.deltaTime);
        transform.Rotate(new Vector3(30, 20, 10)*Time.deltaTime);
    }
}
