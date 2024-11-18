using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionMethod : MonoBehaviour
{
    // 특정 요소를 파라미터 대신. 앞에 참조 하여 마치 해당 객체가 지닌 메소드인것 처럼 사용하는 방법
    private void Start()
    {
        StringHelper.StaticMethod();
        string a = StringHelper.Append("나", "너");
        print(a);
        "나". Append("너");
        string b = "나".Append("너");
        print(b);

        //DateTime today = DateTime.Now;
        //DateTime nextWeek = DateTime.Parse("2024년 11월 25일");

        //print(today.To(nextWeek));
    }
}

//static 클래스 : 따로 객체를 생성하지 않아도 data영역에 변수와 메소드의 정의를 지니고 있는클래스
public static class StringHelper
{
    public static void StaticMethod()
    {
        
    }

    //static 메서드의 첫 파라미터 앞에 this 키워드가 붙으면. 해당 파라미터는 .함수 앞에 참조하여
    //활용 할 수 있다.
    public static string Append(this string prefix, string postfix)
    {
        return prefix + postfix;
    }
}

//public static class DateTimeHelper
//{
//    public static TimeSpan To(this DateTime start, DateTime end)
//    {
//        return start - end;
//    }
//}