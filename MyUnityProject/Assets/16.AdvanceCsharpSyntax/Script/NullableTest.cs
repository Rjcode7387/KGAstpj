using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullableTest : MonoBehaviour
{
    //nullable ���� : ? �����ڸ� ���� Ȱ���ؾ���.

    public bool isBlue;

    private Renderer rend;

    //���ͷ� Ÿ��(��Ÿ��) �ʵ带 ��üó�� null�Ǵ� �ּ�(Instance hash�� ����ϰ� ������
    //���� , C++�� �����Ϳ� ����� ���·� ���� ������ ,type �ڿ�? ���̰� ,�̸� nullable type �̶����

    private int? nullableInt;

    private Vector2? nullableVector;
    

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        //1. 3�� �ǻ��� : bool ? ����true : ����false;
        rend.material.color = isBlue ? Color.blue : Color.red;

        //2. ?. ?? : nullüũ ���
        //a. ��ü?.�Լ�(); : ��ü�� null�� ��� ȣ������ ����.
        MyClass myClass1 = null;
        myClass1?.GetA();
        myClass1 = new MyClass() { a = 1 };
        myClass1?.GetA();
        //b-1. ��ü?.�ʵ� : �ʵ尡 ����Ÿ���� ��쿡�� ����. ��ü�� null�� ��� NullReferenException�� ����� ��� �׳� null�� ����.
        myClass1 = null;
        GameObject someObj = myClass1?.obj;

        print(someObj);

        //b-2. ��ü?.�����ʵ�??�ٸ��ʵ�Ǵ� ��ü : ��ü�� null�� ���, ??���� ���� ���� ��.
        GameObject someObj2 =myClass1?.obj ?? new GameObject();
        print(someObj2);

        //c. ��ü?.���ʵ�??(�ʼ�)��ü �� : ��ü�� null���, �����ϴ� �ʵ尡 ���ͷ� Ÿ���̶�� ������
        //��ü ���� �����Ǿ� ��
        int someInt = myClass1?.a??1;
        print(someInt);

        //if(false == isBlue)
        //{
        //    _=StartCoroutine("");
        //}

        if (myClass1 != null)
        {
            someObj =myClass1.obj;
        }
        else { someObj = new GameObject(); }

        print($"nullableInt : {nullableInt}");

        string intToText = 1.ToString();
        intToText = nullableInt?.ToString()??0.ToString();
        print(intToText);

        nullableInt = 2;

        int localInt = 3;
        nullableInt =localInt;

        localInt = nullableInt.Value;

        nullableInt = null;//nullable ������ null�� �����ؼ� ������ ���

        //localInt =nullableInt.Value;//������ �����غ��� null�̱� ������ ������ �ȵ�

        localInt = nullableInt ?? 0;

        if (nullableInt.HasValue)
        {
            //���������� null check.
            localInt =nullableInt.Value;
        }
        else { localInt =0; }
        
        print(localInt);
    }
    public class MyClass
    {
        public int a;
        public GameObject obj;

        public MyClass() 
        {
            obj = new GameObject();
            obj.name ="myClass";

        }
        public int GetA() { Debug.Log("Return A"); return a; }
    }
}