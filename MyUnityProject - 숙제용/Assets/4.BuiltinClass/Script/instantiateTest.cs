using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateTest : MonoBehaviour
{
    public GameObject original;

    void Start()
    {
        ////original ��ü�� �Ȱ��� ���� ������Ʈ�� �ϳ� �� ����� ���� ��ġ�ϰ� ������.
        //GameObject clone = new GameObject("clone");

        //MeshFilter mf = clone.AddComponent<MeshFilter>();
        //MeshRenderer mr = clone.AddComponent<MeshRenderer>();

        //mf.mesh = original.GetComponent<MeshFilter>().mesh;
        //mr.material = original.GetComponent<MeshRenderer>().material;

        //clone.transform.position = original.transform.position + Vector3.right;
        //�̷��� ��ư� ���� ��������


        //INstantiate : �Ķ���� ��ü�� �Ȱ��� �����ϴ� �Լ�
        //Instantiate(original).transform.position = original.transform.position +Vector3.right;
        //����� ���ÿ� Hierachy�󿡼� Ư�� ��ü�� �ڽ��� �Ǿ�� �� ���

        //Instantiate(original,transform);
        //����� ���ÿ� Ư�� ��ġ�� Ư�� ���������� �����Ǿ�� �� ���
        //Instantiate(original,Vector3.right,Quaternion.identity);
        //Instantiate �Լ��� �Ķ���͸� ���� ������ ��ü�� Return��
         GameObject clone =  Instantiate(original, Vector3.right, Quaternion.identity);
        clone.name = "this is clone";

        clone.GetComponent<MeshRenderer>().material.color = Color.red;


    }
}
