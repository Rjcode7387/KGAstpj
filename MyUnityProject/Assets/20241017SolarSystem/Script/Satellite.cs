using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public GameObject planete;//�༺ ��ü
    public GameObject satellitePrefab;//���� ������
    public int satellitecount;//������ ���� ��
    public float RadiusMin; //�ּ� ���� �ݰ�
    public float RadiusMax; //�ִ� ���� �ݰ�
    public float Movespeed; //���� ���� �ӵ�

    void Start()
    {
        CreateSatellites();//�����Ҷ� ������ �����ϴ� �Լ� ȣ��
    }
    void CreateSatellites()
    {
        //������ ����ŭ ���� ȣ��
        for (int i = 0; i < satellitecount; i++)
        {
            //������ ���� �ݰ� ����
            float Radius = Random.Range(RadiusMin, RadiusMax);
            //������ ������ ����
            float angle = Random.Range(0f, 360f);
            //������ �ʱ� ��ġ ���
            Vector3 position = new Vector3(Mathf.Cos(angle*Mathf.Deg2Rad)*Radius, 0, Mathf.Sin(angle*Mathf.Deg2Rad)*Radius);
            //x��ǥ,0,y��ǥ
            //������ �����ϰ� �ʱ� ��ġ�� ��ġ
            GameObject satellite = Instantiate(satellitePrefab, position+planete.transform.position, Quaternion.identity);
            satellite.transform.parent = planete.transform;
            //������ ���� ��� ���� ������Ʈ �߰�
            SatelliteController controller = satellite.AddComponent<SatelliteController>();
            controller.planet = planete;
            controller.movespeed = Movespeed;
        }
    }
}
//��ũ��Ʈ�� Ŭ������ �ϳ��� ������ ���� ���� ����
//������Ʈ�� �����巡���ؼ� ���ٳ��� ������� �߽����� �༺
public class SatelliteController : MonoBehaviour
{
    public GameObject planet; // �༺ ��ü
    public float movespeed = 30f; // ���� �ӵ�

    void Update()
    {
        // �༺ ������ ����
        transform.RotateAround(planet.transform.position, Vector3.up, movespeed * Time.deltaTime);
        // planet.transform.position�� �߽����� ������ ����
        // ���� �ӵ��� movespeed�� ���� ������
    }
}

    //������ �ٲ㺸��
    //�߽��� ������Ʈ��ü�� �ް� �༺�� ������ ���������������Ʈ�� ����
    //���༺�� ������ ������ �Է�
    //�ִ� ���� �ݰ�� �ּ� ���� �ݰ�
    //������ �����ӵ�

