using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Delegate Ű���� 1 : �븮�� �Լ��� �̸��� ��ü���ش�.
//���������� ������ classó�� ����

//deleqgate�� ���� ���� : [��ȯ��]��������Ʈ�̸�(�Ķ����);

public delegate void SomeMethod(int a);//return�� ���� �Լ�(Method)
public delegate int SomeFunction(int a, int b);

//delegate Ű���� 2 : ���� �޼��� �������� Ȱ��.
public class Delegate : MonoBehaviour
{
    public Text text;
    private void Start()
    {
        SomeMethod myMethod = PrintInt;
        myMethod(1);  // console : 1���
        myMethod += CreateInt;
        myMethod(2);  // console : 2���, 2�̸��� ���� ������Ʈ����
        myMethod-=PrintInt;
        myMethod.Invoke(3);  // 3�̶�� �̸��� ���� ������Ʈ ����
        myMethod -= CreateInt;
        myMethod?.Invoke(4); //myMethod�� null�̸� �׳� ȣ�� ����

        if (myMethod != null)
        {
            myMethod.Invoke(4);
        }

        SomeMethod delegateisclass = new SomeMethod(PrintInt);
        delegateisclass(5);//console : 5���


        //ȣ���� ������ ù��°function�� �ι���function�� ���ϸ� ù��° ������ ������.
        SomeFunction idontknow = Plus;
        int firstReturn = idontknow(1, 2);
        print(firstReturn);

        idontknow += Multiple;
        int secondReturn = idontknow(1, 2);
        print(secondReturn);
        //idontknow += PlusFloat; // ����

        //delegate�� ����޼��� Ȱ��

        //SomeMethod

        SomeMethod someUnnamedMethod = delegate (int a) { text.text = a.ToString(); };

        //1�� ����ȭ : delegate Ű���� ��� => �����ڷ� ��ü
        someUnnamedMethod += (int a) => { print(a); };
        //2�� ����ȭ : �Ķ���� ������Ÿ���� ���� ����.
        someUnnamedMethod +=(b) => { print(b); text.text = b.ToString(); };
        //3�� ����ȭ : �Լ� ������ 1��(�����ݷ� ;�� 1���� ���)�� ���, �߰�ȣ ��������
        someUnnamedMethod += (c) => print(c);
        //�Լ� 1�� ����ȭ�� ��� return Ű������� ���� ����.
        SomeFunction someUnnamedFunction = (someIntA, someIntB) => Plus(someIntA,someIntB);


        someUnnamedMethod(4);

        myMethod += someUnnamedMethod;

        myMethod -= someUnnamedMethod;

        //����޼����� ���� : �ش� �޼��带 ���Ŀ� �ٽ� ������ �� ����.
        //���� ���������� ������ ����

        //string stringA = new string("");
        //string stringB = "";

        //.netFramework ���� delegate
        //1. ������ ���� �Լ�(Method) : Action
        System.Action nonParamMethod = () => { };
        System.Action<int> intParamMethod = (int a) => { };
        System.Action<string> stringParamMethod = (b) => { };
        //2. ������ �ִ� �Լ�(Function) : Func
        System.Func<int> nonParamFunc = () => { return 3; };
        System.Func<int,string>intParamFunc = (int a) => { return a.ToString(); };
        //3. ���ǰ˻縦 ���� ������ bool ������ ���� �Լ� : Predicate
        System.Predicate<int> isSame = (a) => { return a==1; };
        //�� ��
        System.Comparison<Color> compare = ( a, b) => { return (int)(a.a-a.b); };

    }
    private void PrintInt(int a)
    {
        print(a);
    }

    private void CreateInt(int a)
    {
        new GameObject().name = a.ToString();
    }

    private int Plus(int a, int b)
    {
        return a + b;
    }
    private int Multiple(int c, int d)
    {
        return c * d;
    }
    private float PlusFloat(float a, float b)
    {
        return a + b;
    }
}
