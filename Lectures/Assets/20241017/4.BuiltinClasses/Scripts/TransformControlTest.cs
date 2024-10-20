using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControlTest : MonoBehaviour
{
    public Transform target;
    //�⺻������ Transform�� position, rotation, localscale ������Ƽ�� ����
    //�ش� ������Ʈ�� ��ġ , ���� , ũ�� ���� �����Ҽ� �ִ�.
    void Start()
    {
        //ControlByValue();
        //ControlByDirection();
        //ControlByMethod();
    }

    void Update()
    {
        //transform.position = transform.position + new Vector3(3,2,1) * Time.deltaTime;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(30,20,10) * Time.deltaTime);

        transform.Translate(new Vector3(3,2,1)* Time.deltaTime);
        transform.Rotate(new Vector3(30, 20, 10) * Time.deltaTime);
    }

    //���� ���� �����Ͽ� Transform�� ����
    private void ControlByValue() 
    {
        transform.position = new Vector3(3, 2, 1);
        //transform.rotation = new Quaternion(0.3f,0.02f,0.001f,0f);
        transform.rotation = Quaternion.Euler(30, 20, 10);
        transform.localScale = new Vector3(4, 5, 6);
    }

    //����(x : ��,�� y : ��,�� z :��,��)�� ���� ���͸� �����Ͽ� Rotation�� ����
    private void ControlByDirection() 
    {
        //���⺤�͸� ���Ͽ� up�� ���� 
        Vector3 newDirection = target.position - transform.position;
        //�� ��ġ���� Target ��ġ�� �̵��ϱ� ���� ���ؾ��ϴ� ���� ���͸� ����
        //transform.up = target.position - transform.position; �ش� ������ ���� up�� ��

        //�ش� ������ ���� right�� �Ƿ���
        //transform.right = newDirection;

        //�ش� ������ ���� forward�� �Ƿ���
        //transform.forward = newDirection;

        //�ش� ������ ���� down�� �Ƿ���
        //transform.up = transform.up - newDirection;

        //�ش� ������ ���� backward�� �Ƿ���
        //transform.forward = transform.forward - newDirection;

        //�ش� ������ ���� left�� �Ƿ���;
        //transform.right = transform.right - newDirection;    
    }

    //Transform�� ���� �Լ��� ȣ��
    private void ControlByMethod() 
    {
        //Translate : Position�� �����ϴ� �Լ�
        transform.Translate(5,0,0);
        //rotate : rotation�� �����ϴ� �Լ�
        transform.Rotate(30,0,0);
        //Translate , Rotate �Լ��� ����Ͽ� �����ϴ� ����
        //transform.position, rotation�� ���� ���� �Ҵ��ϴ� �Ϳ� �����ڸ�
        //���� position, rotation�� �������� ������� ������� ������ �̷�����°����� �����ϸ� �ȴ�.
    }

    
}
