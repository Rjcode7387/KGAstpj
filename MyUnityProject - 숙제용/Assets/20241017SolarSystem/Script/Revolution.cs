using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{
    public GameObject SUN; //기준이 되는 행성
    public float rotationTime; // 공전속도
    public float selfmovespeed;//자전속도


    private void Update()
    {
        MainSUN();
        Selfspin();
    }

    void MainSUN()
    {   
        transform.RotateAround(SUN.transform.position, Vector3.up, 360f/(rotationTime*31557600f)*Time.deltaTime);
    }
    //기준행성을 정한 오브젝트를 생성한다
    //오브젝트를 타겟으로 잡는다
    //업데이트에 공전 함수를 만든다
    //업데이트에 들어갈 공전 함수에 RotateAround를 사용해보자
    void Selfspin()
    {
        transform.Rotate(Vector3.up, selfmovespeed*Time.deltaTime);
    }
    //자전 함수 설정 
    //자전 함수에 로테이션을 써서 자전 하게 만들기
    //z축이 기울어져있는거는 인스펙터에서 할당해서 변환할거임

}
