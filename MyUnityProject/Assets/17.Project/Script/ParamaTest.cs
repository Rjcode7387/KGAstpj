using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamaTest : MonoBehaviour
{
    private void Start()
    {
        Print("ù ��");
        Print("ù ��", "�ι�° ��");
        Print("ù ��", "�ι�° ��", "����° ��");

        string[] Str = { "�迭 ù ��", "�迭 �ι�° ��", "�迭 ����° ��" };
        Print(Str);


    }
    void Print(params string[] Str)
    {
        foreach (string str in Str)
        {
            //Debug.Log(string.Join("\n", Str));  //string.Join�� �Ἥ ����� �ϳ��� �ٸ��ٷ� ���� ����غ���
            Debug.Log(Str);

        }
    }
}
