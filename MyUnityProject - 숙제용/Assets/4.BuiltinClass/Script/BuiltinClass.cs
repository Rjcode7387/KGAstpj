using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BuiltinClass : MonoBehaviour
{
    //유니티 엔진에서 제공하는 라이브러리에 내장된 클래스를 활용
    //Debug : 디버깅에 사용되는 기능을 제공하는 클래스

    void Start()
    {
        //Debug.Log("log");
        //Debug.LogWarning("");
        //Debug.LogError("");
        //Debug.LogFormat("{0},{1}",3,5.0);//xxFormat으로 끝나는 함수들 : 파라미터를 모멧에 따라 치환하는

        //Debug.DrawLine(Vector3.zero, new Vector3(0, 3), Color.red, 5f);

        //Mathf : UnithEngine에서 제공하는 다양한 수학 연산 함수가 포함된 클래스.

        float a = -0.3f;
        a = Mathf.Abs(a);
        print(a); 
        a= Mathf.Abs(+0.3f);
        print(a);

        //근사값 비교. 실수의 근사값 비교,. 정획히 같은지를 검사할수 없으므로.
        print(1.1f+0.1f == 1.2f);
        print(Mathf.Approximately(1.1f + 0.1f, 1.2f));

        //Mathf.Lerp(a,b,t) : 선형 보간([L]inear Interpolation) :
        //a값과b값 사이의 t의 비율만큼에 위치하는 값.(0<t<1)

        print(Mathf.Lerp(-1f, 1f, 0.5f));
        //Lerp(선형 보간)함수는 Color, Vector(2,3,4),Material 클래스에도 존재함.
        Mathf.InverseLerp(0, 0, 0);//Lerp의 a,b 파라미터를 반대로 쓰고싶을때 쓴다 가독성을 높이기 위한 용도

        //Mathf.Ceil,Floor,Round :올림 ,내림 ,반올림
        float value = 5.5f;

        float ceil = Mathf.Ceil(value);
        float floor = Mathf.Floor(value);
        float round = Mathf.Round(value);

        print($"5.5 Ceil : {ceil}, Floor :{floor}, Round : {round}");

        float sign = Mathf.Sign(value);//부호 
        float sin = Mathf.Sin(value);//삼각함수 사인
        //Mathf.Pow();
        print($"sign : {sign},sin :{sin}");

        //Random : 난수를 생성하는 클래스
        //System.Random random = new System.Random();
        //random.Next();

        //int를 반환하는 Range 함수는 최댓값은 제외하고 반환
        int intRandom = Random.Range(-1, 1);//-1,0,1
        //float를 반환하는 Range 함수는 최대값과 같다고 간주되는 값이 반환될 수도 있음.
        float floatRandom = Random.Range(-1f, 1f);//-1.00~--1 ~ 0.999999...

        float randomValue = Random.value;// == Random.Range(0f,1f);
        //백분울 확률을 편하게 얻기 위해서 사용

        Vector3 randomPosition = Random.insideUnitSphere* 5f;//new Vector3(Random.value*5f,)
        //Vector3(-1~1,-1~1,-1~1); 랜덤한 위치를 뽑고 싶을때 효율적

        Vector3 randomDirection = Random.onUnitSphere;//랜덤한 Vector3가 오는데 길이가 항상 1.
        //랜덤한 "방향"을 뽑고 싶을때 효율적이다.

        Vector2 randomPosition2d = Random.insideUnitCircle;
        //2d용 Random Vector

        //Random.rotation 거꾸로 뒤집을때 사용 (굳이)?
        Random.InitState(12354);//난수의 시드값 초기화
        //연산 부하가 많이 걸리는 함수 이므로, 제한적으로(씬 로딩 초기때쯤이나) 사용할 것.

    }

    //Gizmo : Scene창에서만 볼 수 있는 "기즈모"를 그리는 클래스 .(Debug.DrawXX의 확장기능처럼)


    private void Update()
    {
        Gizmos.DrawCube(Vector3.zero, Vector3.one);//<<--업데이트에서 사용해도 의미없음
        
    }

    //Gizmo 클래스는 OnDrawGizmos,OnDrawGizmosSelected,OnSceneGUI등 Scene창과 에이터에서만 활성화 되는 메시지 함수에서만 유효하게 가능함

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.PI);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector3.one, 0.5f);
    }


}
