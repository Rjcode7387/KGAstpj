using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BuiltinClassesTest : MonoBehaviour
{
    #region 유니티 엔진에서 제공하는 라이브러리에 내장된 클래스를 활용



    void Start()
    {
        #region Debug : 디버깅에 사용되는 기능을 제공하는 클래스
        /*
          Debug.Log($"Log{1}");
          Debug.LogWarning("");
          Debug.LogError("");
          Debug.LogFormat("{0}, {1}", 3, 5.0);//**Format으로 끝나는 함수들 : 파라미터를 포맷에 따라 치환하는 문자열
          Debug.DrawLine(Vector3.zero , new Vector3(0,3),Color.red,5f);
         */
        #endregion

        #region Mathf: UnityEngine에서 제공한느 다양한 수학 연산 메서드가 포함된 클래스.*/

        /*1. Mathf.Abs(float a); : 절대값을 반환*/
        float a = -0.3f;
        a = Mathf.Abs(a);
        print(a);
        a = Mathf.Abs(+0.3f);
        print(a);

        /*2. Mathf.Approximately(float a , float b); : 근사값 비교
             실수의 근사값을 비교한다. 정확히 같은지를 비교할수 없으므로 근사값을 비교하기 위해 사용한다.
         */
        print(1.1f + 0.1f == 1.2f);
        print(Mathf.Approximately(1.1f + 0.1f, 1.2f));

        /*3. Mathf.Lerp(a,b,t); : 선형 보간([L]inear Int[erp]olation) 
             a 값과 b 값 사이의 t 비율만큼에 위치하는 값을 리턴. (0<t<1)
             Lerp(선형보간)함수는 Color, Vector(2,3,4), Material, Quaternion 클래스에도 존재한다.

             Ex)         transform.position = Vector3.Lerp(transform.position, followTarget.position, 
                                                           Time.deltaTime * moveSpeed);
         업데이트문에서 저렇게 한다면 타깃의 포지션과 객체의 포지션의 거리를 비율로 계산하여 프레임이 업데이트될때마다 객체가 위치가 변경된다.
         deltaTime * moveSpeed : 거리는 가까운데 비율인 값은 고정이기에 target에 포지션에 가까워질수록 느려지는 효과를 느낄수 있다.
         */
        print(Mathf.Lerp(-1, 1, 0.5f));
        Mathf.InverseLerp(0, 0, 0); // a, b의 파라미터가 반대

        /*Mathf.Ceil,Floor,Round : 올림, 내림 , 반올림 
         * toint로 정수형으로 반환하수 있다.
         */

        float ceil = Mathf.Ceil(5.5f); 
        float floor = Mathf.Floor(5.5f);
        float round = Mathf.Round(5.5f);

        print($"5,5 , Ceil : {ceil}, Floor : {floor}, Round : {round}");

        //Mathf.Sign(); 부호
        //Mathf.Sin(); 삼각함수 사인
        //Mathf.Pow(); 거듭제곱



        #endregion

        #region Random : 난수를 생성하는 클래스

        /* Tip !
         * using Random = UnityEngine.Random; : System 네임스페이스와 같이 사용할때 대입연산자를 사용하여 Random을 Unity엔진의 Random 클래스만 사용할수 있다.*/

        //1. Random.Range(min, max); : int 형은 최대값을 제외하고 난수가 출력되고 , float형은 정확한 비교가 어렵기 때문에 최대값이 출력될수 있다.
        
        //int를 반환하는 Range 함수는 최대값 제외 반환
        int intRandom = Random.Range(-1,1); // -1 , 0
        //float를 반환하는 Range 함수는 최대값과 같다고 간주되는 값일 반환될 수 있다.
        float floatRandom = Random.Range(-1f,1f); //-1.00 ~ 0.999999999999999999(순환소수)

        //2.Random.value; : 백분율 확률을 편하게 얻기 위함
        float randomValue = Random.value; // == Random.Range(0f,1f);

        //Random.Position
        float size = 5f;
        
        //Vector3(-1~1,-1~1,-1~1); 랜덤한 위치를 뽑고 싶을때 사용        
        Vector3 randomPosition = Random.insideUnitSphere * size;

        //랜덤한 Vector3가 오는데 중앙에서부터의 길이가 항상 1이다. 랜덤한 "방향"을 뽑고 싶을때 효율적이다.
        Vector3 randomonPosition = Random.onUnitSphere * size;

        //2D용 Random Vector
        Vector2 randomPosition2D = Random.insideUnitCircle * size;

        //Random.rotation; (방향)

        //난수의 시드값 초기화
        //연산 부하가 많이 걸리는 함수이므로 , 제한적으로 (씬 로딩 초기때쯤이나) 사용할것.
        Random.InitState(11234);
        #endregion

    }

    #region Gizmo : Scene 창에서만 볼 수 있는 Gizmo를 그리는 클래스.(Debug.Draw** 의 확장기능처럼)


    #endregion
    void Update()
    {
        Gizmos.DrawCube(Vector3.zero, Vector3.one); // <- 의미가 없다.
    }

    //Gizmo 클래스는 OnDrawGizmos, OnDrawGizmosSelected, OnSceneGUI 등 Scene 창에서만 활성화 되는 메시지 함수에서만 유효하게 가능함.

    //on / selected : 해당 객체가 선택됬을때 그릴지 선택한다.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.PI);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Vector3.one, 0.5f);
    }
    #endregion
}
