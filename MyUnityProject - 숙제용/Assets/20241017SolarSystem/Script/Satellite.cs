using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public GameObject planete;//행성 객체
    public GameObject satellitePrefab;//위성 프리펩
    public int satellitecount;//생성할 위성 수
    public float RadiusMin; //최소 공전 반경
    public float RadiusMax; //최대 공전 반경
    public float Movespeed; //위성 공전 속도

    void Start()
    {
        CreateSatellites();//시작할때 위성을 생성하는 함수 호출
    }
    void CreateSatellites()
    {
        //지정된 수만큼 위성 호출
        for (int i = 0; i < satellitecount; i++)
        {
            //랜덤한 공전 반경 생성
            float Radius = Random.Range(RadiusMin, RadiusMax);
            //랜덤한 각도를 선택
            float angle = Random.Range(0f, 360f);
            //위성의 초기 위치 계산
            Vector3 position = new Vector3(Mathf.Cos(angle*Mathf.Deg2Rad)*Radius, 0, Mathf.Sin(angle*Mathf.Deg2Rad)*Radius);
            //x좌표,0,y좌표
            //위성을 생성하고 초기 위치에 배치
            GameObject satellite = Instantiate(satellitePrefab, position+planete.transform.position, Quaternion.identity);
            satellite.transform.parent = planete.transform;
            //위성의 공전 제어를 위한 컴포넌트 추가
            SatelliteController controller = satellite.AddComponent<SatelliteController>();
            controller.planet = planete;
            controller.movespeed = Movespeed;
        }
    }
}
//스크립트에 클래스를 하나만 쓰도록 하자 분할 예정
//컴포넌트에 집접드래그해서 갔다놓을 프리펩과 중심축의 행성
public class SatelliteController : MonoBehaviour
{
    public GameObject planet; // 행성 객체
    public float movespeed = 30f; // 공전 속도

    void Update()
    {
        // 행성 주위를 공전
        transform.RotateAround(planet.transform.position, Vector3.up, movespeed * Time.deltaTime);
        // planet.transform.position을 중심으로 위성이 공전
        // 공전 속도는 movespeed에 의해 조절됨
    }
}

    //생각을 바꿔보자
    //중심할 오브젝트객체를 받고 행성을 공전할 위성프리펩오브젝트를 설정
    //각행성별 생성할 위성수 입력
    //최대 공전 반경과 최소 공전 반경
    //위성의 공전속도

