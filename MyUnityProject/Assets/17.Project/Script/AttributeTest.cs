using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using SSA = SupperAwesomeAttribute;

//그래서 attribute란? : 사전적 의미 속성, 특성, 특징적인 형질
//C#에서의 Attribute는 특정 컨텍스트(클래스 정의, 함수 정의 ,변수의 선언)에 대한 컴파일 타임에서 주어지는 메타데이터.


public class AttributeTest : MonoBehaviour
{
    //Attribute를 사용하는 방법. 대상 컨테스트 앞에 [] 사이에 Attribute 클래스를 상속한 클래스의 이름
    // (에서 Attribute를 뺀 이름)을 적으면 된다.

    [TextArea(4, 15)]
    public string someText;

    [SupperAwesome(getAwesomeMessage = "not awesome",message = "not cool")]
    public int awesomeInt;

}

//개발자가 작성한 커스텀 어트리뷰트 (System.Attribute를 상송한 클래스)앞에
//AttributeUsageAttribute라는 어트리뷰트를 추가하여 어트리뷰트의 사용을 제안하거나 추가 설정이 가능

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Field)]
public class SupperAwesomeAttribute : Attribute
{
    public string message;
    public string getAwesomeMessage;

    public SupperAwesomeAttribute()
    {
        message = "I'm Super Awesome!";
        getAwesomeMessage = "Super Awesome!";
    }

    public SupperAwesomeAttribute(string message)
    {
        this.message=message;
        
    }
}
