using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantianteTest : MonoBehaviour
{
    public GameObject original;
    
    void Start()
    {
        //original ��ü�� �Ȱ��� ���� ������Ʈ�� �ϳ� �� ����� ���� ��ġ�Ϸ� �Ѵ�.

        /*1. �������� ����¹�
        GameObject clone = new GameObject("Clone");

        MeshFilter mf = clone.AddComponent<MeshFilter>();
        MeshRenderer mr = clone.AddComponent<MeshRenderer>();

        mf.mesh = original.GetComponent<MeshFilter>().mesh;
        mr.material = original.GetComponent<MeshRenderer>().material;

        clone.transform.position = original.transform.position + Vector3.right;
        */

        /*1.Instantiate�� ����� ���
          Instantiate : �Ķ���� ��ü�� �Ȱ��� �����ϴ� �Լ�
         */
        //Instantiate(original).transform.position = original.transform.position + Vector3.right;

        //����� ���ÿ� Hierarchy �󿡼� Ư�� ��ü�� �ڽ��� �Ǿ�� �� ���
        //Instantiate(original, transform);

        //����� ���ÿ� Ư�� ��ġ�� Ư�� ��ġ�� ���������� �����ؾ��� ���
        //Instantiate(original, Vector3.right, Quaternion.identity);

        //Instantiate �Լ��� �Ķ���͸� ���� ������ ��ü�� Return�Ѵ�.

        //�����Ǵ� ��ü�� ���ӿ�����Ʈ�� �����Ͽ� �����Ǵ� ��ü�� ������Ʈ�� Ư�� ���� �����Ͽ� �ٲܼ��� �ִ�.
        GameObject clone = Instantiate(original, Vector3.right, Quaternion.identity);
        clone.name = "this is clone";
        //clone.GetComponent<MeshRenderer>().material.color = Color.gray;
    }
}
