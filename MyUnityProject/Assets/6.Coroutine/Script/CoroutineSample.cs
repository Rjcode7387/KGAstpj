using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSample : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(ReturnNull());
        //StartCoroutine(ReturnWaitForSeconds(1f,5));
        //StartCoroutine(ReturnWaitForSecondsRealtime(1f, 5));
        //StartCoroutine(ReturnUntilWhile());
        //StartCoroutine(ReturnEndOfFrame());
        StartCoroutine(_1st());

        //StartCoroutine을 호출을 하면 코루틴을 핸들링하는 객체가 나 자신이 되므로
        //내게임 오브젝트가 비화성화 되거나 Component가 비활성화 되면
        //내가 StartCoroutine을 통해 생성한 모든 코루틴도 동작을 멈춤.
    }

    //yield return null : 매 프레임마다 다음 yield return을 반환
    private IEnumerator ReturnNull()
    {
        print("Return Null 코루틴 스타트.");
        while (true)
        {
            yield return null;
            print($"Return null 코루틴이 수행되었습니다. {Time.time} ");
        }
    }

    //yield return new WaitForSeconds() : 코루틴이 yield return 키워드를
    //만나면 파라미터 만큼 대기 후 수행
    private IEnumerator ReturnWaitForSeconds(float interval,int count)
    {
        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            print($"Wait For Seconds{i+1}번 호출됨. {Time.time}");
        }
        print("Return Wait For Seconds 코루틴이 끝났습니다.");

    }
    //yield return new WaitForSecondsRealtime(param) :
    //WaitForSeconds와 동작은 같으나 Timescale의 영향을 받지않는다

    private IEnumerator ReturnWaitForSecondsRealtime(float interval, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSecondsRealtime(interval);
            print($"Wait For Seconds Realtime{i+1}번 호출됨. {Time.time}");
        }
        print("Return Wait For Seconds Realtime 코루틴이 끝났습니다.");
    }

    public bool continueKey;

    private bool IsContienue()
    {
        return continueKey;
    }

    //yield return new WailUntil/Waitwhile (param) : 특정 조건이 True/False가 될때까지 대기
    private IEnumerator ReturnUntilWhile()
    {
        print("Return Until While 코루틴 시작됨");
        while (true)
        {

            //new WaitUntil : 매개변수로 넘어간 함수(대리자)의 return이
            //false인 동안 대기,true면 넘어감
            yield return new WaitUntil(() => continueKey);
            print("참참참");
            //new WaitWhile : 매개변수로 넘어간 함수(대리자)의 return이 
            //true인동안 대기,false면넘어감
            yield return new WaitWhile(IsContienue);
            print("거짓거짓");
        }
    }

    //yield return new(Frame  계열) :  인 게임의 특징 프레임 뒤에 수정됨.

    private IEnumerator ReturnEndOfFrame()
    {
        //EndOfFrame : 해당 프레임의 가장 마지막까지 기다림.
        yield return new WaitForEndOfFrame();
        print("End of Frame");
        isFirstFram =false;

    }

    private IEnumerator ReturnFixedUpdate()
    {
        //FixedUpdate :물리연산이 끝날 때가지 기다림.
        yield return new WaitForFixedUpdate();
    }

    bool isFirstFram = false;

    private void Update()
    {
        if (isFirstFram)
        {
            print("Update 메시지 함수 호출");
        }
    }
    private void LateUpdate()
    {
        print("LateUpdate 메시지 함수 호출");
    }

    //yield return 코루틴 : 해단 코루틴이 끝날때까지 대기.

    private IEnumerator _1st()
    {
        print("1번째 코루틴이 시작됨");
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"1번째 코루틴이{i+1} 번 수행됨 ");
        }
        print("1번째 코루틴이 2번째 코루틴을 시작하고 대기함");
        yield return StartCoroutine(_2nd());
        print("1번째 코루틴이 끝남");
    }

    private IEnumerator _2nd()
    {
        print("2번째 코루틴 시작됨");
       
       
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"2번째 코루틴{i+1}번째 수행됨");
           
        }
        print("2번째 코루틴이 3번째 코루틴을 시작하고 대기함");
        yield return StartCoroutine(_3nd());
        print("2번째 코루틴이 끝남");
    }
    private IEnumerator _3nd()
    {
        print("3번째 코루틴 스타트");
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"3번째 코루틴{i+1}번째 수행됨");
        }
        print("3번째 코루틴 끝남");
    }

}
