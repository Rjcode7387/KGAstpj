using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//C# Attribute : Ư�� ���(Ŭ����, ���� , �Լ�)�� ���� ��Ÿ�����͸� ����
public class Myclass // �� Ŭ������ ���� �ٸ� ��������� �����ϱ����ؼ��� "����ȭ"�� �ʿ��ϴ�.
{
    public string name;
    public int id;
    public Sprite sprite;
    
}
public class ReferenceVariable : MonoBehaviour
{
   public Myclass myclass;

    public List<Myclass> myclasses;
    // Start is called before the first frame update
    void Start()
    {
        print(myclass); // print() = debug.log()
        print(myclass.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
