using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{
    public GameObject SUN; //������ �Ǵ� �༺
    public float rotationTime; // �����ӵ�
    public float selfmovespeed;//�����ӵ�


    private void Update()
    {
        MainSUN();
        Selfspin();
    }

    void MainSUN()
    {   
        transform.RotateAround(SUN.transform.position, Vector3.up, 360f/(rotationTime*31557600f)*Time.deltaTime);
    }
    //�����༺�� ���� ������Ʈ�� �����Ѵ�
    //������Ʈ�� Ÿ������ ��´�
    //������Ʈ�� ���� �Լ��� �����
    //������Ʈ�� �� ���� �Լ��� RotateAround�� ����غ���
    void Selfspin()
    {
        transform.Rotate(Vector3.up, selfmovespeed*Time.deltaTime);
    }
    //���� �Լ� ���� 
    //���� �Լ��� �����̼��� �Ἥ ���� �ϰ� �����
    //z���� �������ִ°Ŵ� �ν����Ϳ��� �Ҵ��ؼ� ��ȯ�Ұ���

}
