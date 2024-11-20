using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControl : MonoBehaviour
{
    public Transform target;

    //�⺻������ Transform�� position, rotation,localScale ������Ƽ�� ����
    //�ش� ������Ʈ�� ��ġ,���� , ũ����� ���� �� �� �ִ�.

    private void Start()
    {
        //ControlDirect();
        //ControlByDirection();
        ControlByMethod();

    }

    //���� ���� �����Ͽ� Transform�� ����
    private void ControlDirect()
    {
        transform.position = new Vector3(3, 2, 1);
        //transform.rotation = new Quaternion(0.3f, 0.02f, 0.001f, 0);
        transform.rotation = Quaternion.Euler(30, 20, 10);
        print(transform.rotation);
        transform.localScale = new Vector3(4, 5, 6);

    }


    //����(��,��,��,��,��,��) x��=����, y�� =�¿�, z�� = ����
    private void ControlByDirection()
    {

        Vector3 lookdir = target.position - transform.position; 
        //�� ��ġ���� Target ��ġ�� �̵��ϱ����� ���ؾ� �ϴ� ���� ���͸� ����

        //transform.up = target.position - transform.position;

        //�ش� ������ ���� right�� �Ƿ���?
        //transform.right = lookdir;
        //�ش� ������ ���� forward�� �Ƿ���?
        //transform.forward = lookdir;



        // ���� �غ���
        //�ش� ������ ���� down�� �Ƿ���?
      
        //�ش� ������ ���� left�� �Ƿ���?
        
        //�ش� ������ ���� backward�� �Ƿ���?

    }


    //Transform�� ���� �Լ��� ȣ�� 
    private void ControlByMethod()
    {
        //Translate : Position�� �����ϴ� �Լ�
        transform.Translate(5,0,0);
        //Rotate : Rotation�� �����ϴ� �Լ�
        transform.Rotate(30,0,0);

        //Translate, Rotate �Լ��� ����Ͽ� �����ϴ� ����
        //transform.position,rotaion�� ���� ���� �Ҵ��ϴ� �Ͱ� �� ���ڸ�
        //���� position, rotation �������� ������� ������ �̷������ ������ �����ϸ� �ȴ�.

    }



    //public Vector3 lookDir;
    
    private void Update()
    {
        //transform.position = transform.position+ new Vector3(3, 2, 1)*Time.deltaTime;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles+ new Vector3(30,20,10)*Time.deltaTime);

        transform.Translate(new Vector3(3, 2, 1)*Time.deltaTime);
        transform.Rotate(new Vector3(30, 20, 10)*Time.deltaTime);
    }
}
