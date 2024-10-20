using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BuiltinClassesTest : MonoBehaviour
{
    #region ����Ƽ �������� �����ϴ� ���̺귯���� ����� Ŭ������ Ȱ��



    void Start()
    {
        #region Debug : ����뿡 ���Ǵ� ����� �����ϴ� Ŭ����
        /*
          Debug.Log($"Log{1}");
          Debug.LogWarning("");
          Debug.LogError("");
          Debug.LogFormat("{0}, {1}", 3, 5.0);//**Format���� ������ �Լ��� : �Ķ���͸� ���˿� ���� ġȯ�ϴ� ���ڿ�
          Debug.DrawLine(Vector3.zero , new Vector3(0,3),Color.red,5f);
         */
        #endregion

        #region Mathf: UnityEngine���� �����Ѵ� �پ��� ���� ���� �޼��尡 ���Ե� Ŭ����.*/

        /*1. Mathf.Abs(float a); : ���밪�� ��ȯ*/
        float a = -0.3f;
        a = Mathf.Abs(a);
        print(a);
        a = Mathf.Abs(+0.3f);
        print(a);

        /*2. Mathf.Approximately(float a , float b); : �ٻ簪 ��
             �Ǽ��� �ٻ簪�� ���Ѵ�. ��Ȯ�� �������� ���Ҽ� �����Ƿ� �ٻ簪�� ���ϱ� ���� ����Ѵ�.
         */
        print(1.1f + 0.1f == 1.2f);
        print(Mathf.Approximately(1.1f + 0.1f, 1.2f));

        /*3. Mathf.Lerp(a,b,t); : ���� ����([L]inear Int[erp]olation) 
             a ���� b �� ������ t ������ŭ�� ��ġ�ϴ� ���� ����. (0<t<1)
             Lerp(��������)�Լ��� Color, Vector(2,3,4), Material, Quaternion Ŭ�������� �����Ѵ�.

             Ex)         transform.position = Vector3.Lerp(transform.position, followTarget.position, 
                                                           Time.deltaTime * moveSpeed);
         ������Ʈ������ ������ �Ѵٸ� Ÿ���� �����ǰ� ��ü�� �������� �Ÿ��� ������ ����Ͽ� �������� ������Ʈ�ɶ����� ��ü�� ��ġ�� ����ȴ�.
         deltaTime * moveSpeed : �Ÿ��� ���� ������ ���� �����̱⿡ target�� �����ǿ� ����������� �������� ȿ���� ������ �ִ�.
         */
        print(Mathf.Lerp(-1, 1, 0.5f));
        Mathf.InverseLerp(0, 0, 0); // a, b�� �Ķ���Ͱ� �ݴ�

        /*Mathf.Ceil,Floor,Round : �ø�, ���� , �ݿø� 
         * toint�� ���������� ��ȯ�ϼ� �ִ�.
         */

        float ceil = Mathf.Ceil(5.5f); 
        float floor = Mathf.Floor(5.5f);
        float round = Mathf.Round(5.5f);

        print($"5,5 , Ceil : {ceil}, Floor : {floor}, Round : {round}");

        //Mathf.Sign(); ��ȣ
        //Mathf.Sin(); �ﰢ�Լ� ����
        //Mathf.Pow(); �ŵ�����



        #endregion

        #region Random : ������ �����ϴ� Ŭ����

        /* Tip !
         * using Random = UnityEngine.Random; : System ���ӽ����̽��� ���� ����Ҷ� ���Կ����ڸ� ����Ͽ� Random�� Unity������ Random Ŭ������ ����Ҽ� �ִ�.*/

        //1. Random.Range(min, max); : int ���� �ִ밪�� �����ϰ� ������ ��µǰ� , float���� ��Ȯ�� �񱳰� ��Ʊ� ������ �ִ밪�� ��µɼ� �ִ�.
        
        //int�� ��ȯ�ϴ� Range �Լ��� �ִ밪 ���� ��ȯ
        int intRandom = Random.Range(-1,1); // -1 , 0
        //float�� ��ȯ�ϴ� Range �Լ��� �ִ밪�� ���ٰ� ���ֵǴ� ���� ��ȯ�� �� �ִ�.
        float floatRandom = Random.Range(-1f,1f); //-1.00 ~ 0.999999999999999999(��ȯ�Ҽ�)

        //2.Random.value; : ����� Ȯ���� ���ϰ� ��� ����
        float randomValue = Random.value; // == Random.Range(0f,1f);

        //Random.Position
        float size = 5f;
        
        //Vector3(-1~1,-1~1,-1~1); ������ ��ġ�� �̰� ������ ���        
        Vector3 randomPosition = Random.insideUnitSphere * size;

        //������ Vector3�� ���µ� �߾ӿ��������� ���̰� �׻� 1�̴�. ������ "����"�� �̰� ������ ȿ�����̴�.
        Vector3 randomonPosition = Random.onUnitSphere * size;

        //2D�� Random Vector
        Vector2 randomPosition2D = Random.insideUnitCircle * size;

        //Random.rotation; (����)

        //������ �õ尪 �ʱ�ȭ
        //���� ���ϰ� ���� �ɸ��� �Լ��̹Ƿ� , ���������� (�� �ε� �ʱ⶧���̳�) ����Ұ�.
        Random.InitState(11234);
        #endregion

    }

    #region Gizmo : Scene â������ �� �� �ִ� Gizmo�� �׸��� Ŭ����.(Debug.Draw** �� Ȯ����ó��)


    #endregion
    void Update()
    {
        Gizmos.DrawCube(Vector3.zero, Vector3.one); // <- �ǹ̰� ����.
    }

    //Gizmo Ŭ������ OnDrawGizmos, OnDrawGizmosSelected, OnSceneGUI �� Scene â������ Ȱ��ȭ �Ǵ� �޽��� �Լ������� ��ȿ�ϰ� ������.

    //on / selected : �ش� ��ü�� ���É����� �׸��� �����Ѵ�.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.PI);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Vector3.one, 0.5f);
    }
    #endregion
}
