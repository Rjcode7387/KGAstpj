using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BuiltinClass : MonoBehaviour
{
    //����Ƽ �������� �����ϴ� ���̺귯���� ����� Ŭ������ Ȱ��
    //Debug : ����뿡 ���Ǵ� ����� �����ϴ� Ŭ����

    void Start()
    {
        //Debug.Log("log");
        //Debug.LogWarning("");
        //Debug.LogError("");
        //Debug.LogFormat("{0},{1}",3,5.0);//xxFormat���� ������ �Լ��� : �Ķ���͸� ��信 ���� ġȯ�ϴ�

        //Debug.DrawLine(Vector3.zero, new Vector3(0, 3), Color.red, 5f);

        //Mathf : UnithEngine���� �����ϴ� �پ��� ���� ���� �Լ��� ���Ե� Ŭ����.

        float a = -0.3f;
        a = Mathf.Abs(a);
        print(a); 
        a= Mathf.Abs(+0.3f);
        print(a);

        //�ٻ簪 ��. �Ǽ��� �ٻ簪 ��,. ��ȹ�� �������� �˻��Ҽ� �����Ƿ�.
        print(1.1f+0.1f == 1.2f);
        print(Mathf.Approximately(1.1f + 0.1f, 1.2f));

        //Mathf.Lerp(a,b,t) : ���� ����([L]inear Interpolation) :
        //a����b�� ������ t�� ������ŭ�� ��ġ�ϴ� ��.(0<t<1)

        print(Mathf.Lerp(-1f, 1f, 0.5f));
        //Lerp(���� ����)�Լ��� Color, Vector(2,3,4),Material Ŭ�������� ������.
        Mathf.InverseLerp(0, 0, 0);//Lerp�� a,b �Ķ���͸� �ݴ�� ��������� ���� �������� ���̱� ���� �뵵

        //Mathf.Ceil,Floor,Round :�ø� ,���� ,�ݿø�
        float value = 5.5f;

        float ceil = Mathf.Ceil(value);
        float floor = Mathf.Floor(value);
        float round = Mathf.Round(value);

        print($"5.5 Ceil : {ceil}, Floor :{floor}, Round : {round}");

        float sign = Mathf.Sign(value);//��ȣ 
        float sin = Mathf.Sin(value);//�ﰢ�Լ� ����
        //Mathf.Pow();
        print($"sign : {sign},sin :{sin}");

        //Random : ������ �����ϴ� Ŭ����
        //System.Random random = new System.Random();
        //random.Next();

        //int�� ��ȯ�ϴ� Range �Լ��� �ִ��� �����ϰ� ��ȯ
        int intRandom = Random.Range(-1, 1);//-1,0,1
        //float�� ��ȯ�ϴ� Range �Լ��� �ִ밪�� ���ٰ� ���ֵǴ� ���� ��ȯ�� ���� ����.
        float floatRandom = Random.Range(-1f, 1f);//-1.00~--1 ~ 0.999999...

        float randomValue = Random.value;// == Random.Range(0f,1f);
        //��п� Ȯ���� ���ϰ� ��� ���ؼ� ���

        Vector3 randomPosition = Random.insideUnitSphere* 5f;//new Vector3(Random.value*5f,)
        //Vector3(-1~1,-1~1,-1~1); ������ ��ġ�� �̰� ������ ȿ����

        Vector3 randomDirection = Random.onUnitSphere;//������ Vector3�� ���µ� ���̰� �׻� 1.
        //������ "����"�� �̰� ������ ȿ�����̴�.

        Vector2 randomPosition2d = Random.insideUnitCircle;
        //2d�� Random Vector

        //Random.rotation �Ųٷ� �������� ��� (����)?
        Random.InitState(12354);//������ �õ尪 �ʱ�ȭ
        //���� ���ϰ� ���� �ɸ��� �Լ� �̹Ƿ�, ����������(�� �ε� �ʱ⶧���̳�) ����� ��.

    }

    //Gizmo : Sceneâ������ �� �� �ִ� "�����"�� �׸��� Ŭ���� .(Debug.DrawXX�� Ȯ����ó��)


    private void Update()
    {
        Gizmos.DrawCube(Vector3.zero, Vector3.one);//<<--������Ʈ���� ����ص� �ǹ̾���
        
    }

    //Gizmo Ŭ������ OnDrawGizmos,OnDrawGizmosSelected,OnSceneGUI�� Sceneâ�� �����Ϳ����� Ȱ��ȭ �Ǵ� �޽��� �Լ������� ��ȿ�ϰ� ������

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.PI);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector3.one, 0.5f);
    }


}
