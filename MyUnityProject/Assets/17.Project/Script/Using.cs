// using Ű����.
//1. �ܺ��� ���̺귯�� �� ���� ����� ���̺귯���� �߰�
//C/C++�� #include�� ����м� , Java�� import�� �Ȱ���.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using Rn = UnityEngine.Random;

//2.�� .cs�ؽ�Ʈ ���Ͽ����� ����� ���ؽ�Ʈ(Ŭ����, ����ü ,�븮�� ��)�̸��� ������ �� ����

public class Using : MonoBehaviour
{
    //~ : C++�� �Ҹ��ڿ� ���� �Ҹ��� ��ü�� C#������ ���ǰ� �����ϳ�.
    //�⺻������ IDisposable �������̽��� ���� Dispoase()�� ȣ���ϵ��� ��.
    private void Start()
    {
        //3 IDisposable �������̽��� ������ ��ü�� Ư�� ��� �������� ������ �� �Ŀ�
        //��� ������ �Ͻ��� �޸𸮸� ��ü�ϵ��� �ϴ� ���.
        using (HttpClient httpClient = new HttpClient())
        {

        }
        

            HttpClient client = new HttpClient();
        //~~
        client.Dispose();
      
    }

}
