using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Reflection : System.Reflection 네임스페이스에 포함된 기능 전반.
//컴파일 타임에서 생성된 클래스,메소드 , 멤버변수 등 여러 컨텍스트에 대한 데이터를
//색안하고 취급하는 기능.
//Attribute는 컴파일타임에서 생성하는 메타데이터이므로 리플렉션을 통해 데이터를 가져올 수 있다.


[RequireComponent(typeof(AttributeTest))]
public class ReflectionTest : MonoBehaviour
{
    AttributeTest attTest;
    private void Awake()
    {
        attTest = GetComponent<AttributeTest>();
    }

    private void Start()
    {
        //attTest의 타입을 확인
        MonoBehaviour attTestBoxingForm = attTest;
        Type attTestType = attTestBoxingForm.GetType();
        print(attTestType);

        //AttributeTest 라는 클래스의 데이터를 오목조목 따져보는 시간
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;

        //public으로 접근이 가능한 동시에 Static이 아니라 객체별로 field또는 propertie
        //attTestType : attTest의 GetType을 통해 클래스 명세에 대한 데이터를 가지고 있음.
        FieldInfo[] fieldInfos = attTestType.GetFields(bind);
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            print($"{fieldInfo.Name}의 타입은 {fieldInfo.FieldType}");

            SupperAwesomeAttribute attribute = fieldInfo.GetCustomAttribute<SupperAwesomeAttribute>();
           

            if (attribute is null)
            {
                print($"{fieldInfo.Name}에는 슈썸 어트리뷰트가 없습니다");
                continue;
            }
            print($"{fieldInfo.Name}에는 슈섭 어트리뷰트가 있습니다.!");
            print($"{attribute.getAwesomeMessage}, {attribute.message}");
            print($"{fieldInfo.GetValue(attTest)}");
        }

    }
}
