using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using SSA = SupperAwesomeAttribute;

//�׷��� attribute��? : ������ �ǹ� �Ӽ�, Ư��, Ư¡���� ����
//C#������ Attribute�� Ư�� ���ؽ�Ʈ(Ŭ���� ����, �Լ� ���� ,������ ����)�� ���� ������ Ÿ�ӿ��� �־����� ��Ÿ������.


public class AttributeTest : MonoBehaviour
{
    //Attribute�� ����ϴ� ���. ��� ���׽�Ʈ �տ� [] ���̿� Attribute Ŭ������ ����� Ŭ������ �̸�
    // (���� Attribute�� �� �̸�)�� ������ �ȴ�.

    [TextArea(4, 15)]
    public string someText;

    [SupperAwesome(getAwesomeMessage = "not awesome",message = "not cool")]
    public int awesomeInt;

}

//�����ڰ� �ۼ��� Ŀ���� ��Ʈ����Ʈ (System.Attribute�� ����� Ŭ����)�տ�
//AttributeUsageAttribute��� ��Ʈ����Ʈ�� �߰��Ͽ� ��Ʈ����Ʈ�� ����� �����ϰų� �߰� ������ ����

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
