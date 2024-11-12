using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouroutineStarer : MonoBehaviour
{
    //StartCoroutine�� ȣ���� �ϸ� �ڷ�ƾ�� �ڵ鸵�ϴ� ��ü�� �� �ڽ��� �ǹǷ�
    //������ ������Ʈ�� ��ȭ��ȭ �ǰų� Component�� ��Ȱ��ȭ �Ǹ�
    //���� StartCoroutine�� ���� ������ ��� �ڷ�ƾ�� ������ ����.
    public NewBehaviourScript target;

    private void Start()
    {
        target.StartCoroutine(DamageOnTime());
    }

    private IEnumerator DamageOnTime()
    {
        print($"{name}��{target.name}���� ��Ʈ �������� �־����ϴ�.");

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"{target.name}: �ƾ�!x{i}"); 
        }
    }
}
