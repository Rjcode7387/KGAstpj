using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Reflection : System.Reflection ���ӽ����̽��� ���Ե� ��� ����.
//������ Ÿ�ӿ��� ������ Ŭ����,�޼ҵ� , ������� �� ���� ���ؽ�Ʈ�� ���� �����͸�
//�����ϰ� ����ϴ� ���.
//Attribute�� ������Ÿ�ӿ��� �����ϴ� ��Ÿ�������̹Ƿ� ���÷����� ���� �����͸� ������ �� �ִ�.


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
        //attTest�� Ÿ���� Ȯ��
        MonoBehaviour attTestBoxingForm = attTest;
        Type attTestType = attTestBoxingForm.GetType();
        print(attTestType);

        //AttributeTest ��� Ŭ������ �����͸� �������� �������� �ð�
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;

        //public���� ������ ������ ���ÿ� Static�� �ƴ϶� ��ü���� field�Ǵ� propertie
        //attTestType : attTest�� GetType�� ���� Ŭ���� ���� ���� �����͸� ������ ����.
        FieldInfo[] fieldInfos = attTestType.GetFields(bind);
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            print($"{fieldInfo.Name}�� Ÿ���� {fieldInfo.FieldType}");

            SupperAwesomeAttribute attribute = fieldInfo.GetCustomAttribute<SupperAwesomeAttribute>();
           

            if (attribute is null)
            {
                print($"{fieldInfo.Name}���� ���� ��Ʈ����Ʈ�� �����ϴ�");
                continue;
            }
            print($"{fieldInfo.Name}���� ���� ��Ʈ����Ʈ�� �ֽ��ϴ�.!");
            print($"{attribute.getAwesomeMessage}, {attribute.message}");
            print($"{fieldInfo.GetValue(attTest)}");
        }

    }
}
