using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    //Transform : �⺻������ ���� ��ü�� ���� ��� GameObject���� ������ 1���� Transform�� ����

    public GameObject yourObject;

    public Transform parent;

    public Transform grandParent;
    private void Start()
    {
        //��� GameObject, Component Ŭ������ transform�̶�� ������Ƽ��
        //�ش� ��ü�� Transform �����ʴ¸� ��ȯ
        print($"my transform : {transform}");
        print($"your transform : {yourObject.transform}");
        print($"my transform's transform : {transform.transform}");//�������� 

        string somChildName = parent.Find("ThirdChild").GetChild(0).name;
        print(somChildName);

        parent.SetParent(grandParent,false);
        //parent.parent = grandParent; // =>�Ȱ��� ���� �ϳ�,  �Ϲ�������SetParent �Լ��� ȣ����


    }
}
