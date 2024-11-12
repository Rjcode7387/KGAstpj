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

        //StartCoroutine�� ȣ���� �ϸ� �ڷ�ƾ�� �ڵ鸵�ϴ� ��ü�� �� �ڽ��� �ǹǷ�
        //������ ������Ʈ�� ��ȭ��ȭ �ǰų� Component�� ��Ȱ��ȭ �Ǹ�
        //���� StartCoroutine�� ���� ������ ��� �ڷ�ƾ�� ������ ����.
    }

    //yield return null : �� �����Ӹ��� ���� yield return�� ��ȯ
    private IEnumerator ReturnNull()
    {
        print("Return Null �ڷ�ƾ ��ŸƮ.");
        while (true)
        {
            yield return null;
            print($"Return null �ڷ�ƾ�� ����Ǿ����ϴ�. {Time.time} ");
        }
    }

    //yield return new WaitForSeconds() : �ڷ�ƾ�� yield return Ű���带
    //������ �Ķ���� ��ŭ ��� �� ����
    private IEnumerator ReturnWaitForSeconds(float interval,int count)
    {
        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            print($"Wait For Seconds{i+1}�� ȣ���. {Time.time}");
        }
        print("Return Wait For Seconds �ڷ�ƾ�� �������ϴ�.");

    }
    //yield return new WaitForSecondsRealtime(param) :
    //WaitForSeconds�� ������ ������ Timescale�� ������ �����ʴ´�

    private IEnumerator ReturnWaitForSecondsRealtime(float interval, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSecondsRealtime(interval);
            print($"Wait For Seconds Realtime{i+1}�� ȣ���. {Time.time}");
        }
        print("Return Wait For Seconds Realtime �ڷ�ƾ�� �������ϴ�.");
    }

    public bool continueKey;

    private bool IsContienue()
    {
        return continueKey;
    }

    //yield return new WailUntil/Waitwhile (param) : Ư�� ������ True/False�� �ɶ����� ���
    private IEnumerator ReturnUntilWhile()
    {
        print("Return Until While �ڷ�ƾ ���۵�");
        while (true)
        {

            //new WaitUntil : �Ű������� �Ѿ �Լ�(�븮��)�� return��
            //false�� ���� ���,true�� �Ѿ
            yield return new WaitUntil(() => continueKey);
            print("������");
            //new WaitWhile : �Ű������� �Ѿ �Լ�(�븮��)�� return�� 
            //true�ε��� ���,false��Ѿ
            yield return new WaitWhile(IsContienue);
            print("��������");
        }
    }

    //yield return new(Frame  �迭) :  �� ������ Ư¡ ������ �ڿ� ������.

    private IEnumerator ReturnEndOfFrame()
    {
        //EndOfFrame : �ش� �������� ���� ���������� ��ٸ�.
        yield return new WaitForEndOfFrame();
        print("End of Frame");
        isFirstFram =false;

    }

    private IEnumerator ReturnFixedUpdate()
    {
        //FixedUpdate :���������� ���� ������ ��ٸ�.
        yield return new WaitForFixedUpdate();
    }

    bool isFirstFram = false;

    private void Update()
    {
        if (isFirstFram)
        {
            print("Update �޽��� �Լ� ȣ��");
        }
    }
    private void LateUpdate()
    {
        print("LateUpdate �޽��� �Լ� ȣ��");
    }

    //yield return �ڷ�ƾ : �ش� �ڷ�ƾ�� ���������� ���.

    private IEnumerator _1st()
    {
        print("1��° �ڷ�ƾ�� ���۵�");
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"1��° �ڷ�ƾ��{i+1} �� ����� ");
        }
        print("1��° �ڷ�ƾ�� 2��° �ڷ�ƾ�� �����ϰ� �����");
        yield return StartCoroutine(_2nd());
        print("1��° �ڷ�ƾ�� ����");
    }

    private IEnumerator _2nd()
    {
        print("2��° �ڷ�ƾ ���۵�");
       
       
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"2��° �ڷ�ƾ{i+1}��° �����");
           
        }
        print("2��° �ڷ�ƾ�� 3��° �ڷ�ƾ�� �����ϰ� �����");
        yield return StartCoroutine(_3nd());
        print("2��° �ڷ�ƾ�� ����");
    }
    private IEnumerator _3nd()
    {
        print("3��° �ڷ�ƾ ��ŸƮ");
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"3��° �ڷ�ƾ{i+1}��° �����");
        }
        print("3��° �ڷ�ƾ ����");
    }

}
