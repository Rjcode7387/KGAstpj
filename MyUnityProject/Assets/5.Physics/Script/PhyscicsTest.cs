using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyscicsTest : MonoBehaviour
{
    //�� ��ũ��Ʈ�� �޷��ִ� ��ü�� �����̰� ������
    public float moveSpeed;
    public float jumpPorwer;
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //���� ������ �Ͼ�� �������� Update���� �̵��� ��ġ�� ������ �Ͼ��
        //���������� ���� Rigedbody�� ���� ����
        //transform.Translate(new Vector3(x, 0, z)*Time.deltaTime*moveSpeed);

        rb.MovePosition(transform.position +(new Vector3(x, 0, z)*Time.deltaTime*moveSpeed));

        if (Input.GetButtonDown("Jump"))
        {
            //transform.Translate(Vector3.up);
            //rb.velocity = Vector3.up*jumpPorwer;
            rb.AddForce(Vector3.up*jumpPorwer, ForceMode.VelocityChange);

            //���� ���Ҷ� AddForce �Լ��� ��� 
            //ForceMode
            //             �߷� ����              �߷�����
            //���ӵ� �߰�   .Force              .Acceleration
            //��� �߰�   .Impulse            .VelocityChange

            //rb.AddTorque(); //ȸ��
            //rb.angularVelocity //ȸ�� ���
            //rb.maxAngularVelocity; //�ִ� ȸ�� ����� ����
            //rb.maxLinearVelocity   //�ִ� ���� ����� ����
            //rb.drag           //(����)����
            //rb.angularDrag    // ȸ�� ����


        }

        //Physocs.Raycast

        if (Input.GetButtonDown("Fire1"))
        {
            //����(+z����)�� �ִ� �ݶ��̴��� �����ؼ� ���� Enemy�±װ� ������ ���ְ� �� �Ӥ���

            Ray ray = new Ray(transform.position,Vector3.forward);
            Debug.DrawRay(ray.origin,ray.direction*10,Color.red,0.2f);

            Physics.Raycast(ray,out RaycastHit hit ,10,1<<LayerMask.NameToLayer("Default"));
            {

                print($"�ݶ��̴� ���� : {hit.collider.name}");

                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Enemy �±׸� ���� ������Ʈ�� �浹 �ϸ� -z�������� ƨ�ܳ����� �ʹ�
            rb.AddForce(Vector3.back * 50f,ForceMode.Impulse);
        }
    }


    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //����
        //InputManager�� ���� �Է� ��� �Ϸ��� �� ���
        //��� �Է� ó���� Update���� �̷������ ������
        //FoxedUpdate������ ��Ȯ�� ������ �˱� �����

        //if (Input.GetButtonDown("Jump"))
        //{
        //    //transform.Translate(Vector3.up);
        //}

        //transform.Translate(new Vector3(x, 0, z)*Time.deltaTime*moveSpeed);

    }
}
