using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    MeshRenderer mr;

    public Material woodMaterial;
    public Material redMaterial;
    public Material stoneMaterial;

    public Transform transformTest;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        stoneMaterial = mr.material;
    }
    void Start()
    {
        //��ȹ�� 3�� �Ŀ� MeshRenderer�� Material�� woodMaterial�� ��ü �ϰ�;��.
        var enumerator = StringEnumerator();

        while (enumerator.MoveNext())
        {
            print(enumerator.Current);
        }


        //enumerator.MoveNext();
        //print(enumerator.Current);
        //enumerator.MoveNext();
        //print(enumerator.Current);

        //List<int> intlist = new List<int>() {1,2,3};

        //foreach (int some in intlist)
        //{
        //    print(someInt);
        //}
        //foreach (Transform someTransform in transformTest)
        //{
        //    print(someTransform.name);
        //}
        //�ϼ���
        // StartCoroutine("MaterialChange");
        //�����
        //�ڷ�ƾ ��ü�� ������
        materialChangeCoroutine = StartCoroutine(MaterialChange(redMaterial,1f));
    }
    private Coroutine materialChangeCoroutine;



    void Update()
    {
        //if (Time.time > 3f)
        //{
        //    mr.material = woodMaterial;
        //}
        if (Input.GetButtonDown("Jump"))
        {
            mr.material = stoneMaterial;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            print("�ڷ�ƾ ��ž");
            StopCoroutine(materialChangeCoroutine);
        }
    }
    private IEnumerator<string> StringEnumerator()
    {
        //IEnumerator�� ��ȯ�ϴ� �Լ���
        //yield return Ű���带 ����
        //���� ���������� ��ȯ �� �� ����.
        yield return "A";
        yield return "B";
        yield return "C";//yield�纸�ϴ�
    }

    private IEnumerator MaterialChange(Material mat,float interval )
    {
        while (true)
        {
            //�ڷ�ƾ�� 3�ʵ��� ��� �մϴ�.
            yield return new WaitForSeconds(interval);
            mr.material = mat;
        }


        //while (true)
        //{
        //    yield return null;
        //    print("MaterialChange �ڷ�ƾ �����");
        //}
    }

}
