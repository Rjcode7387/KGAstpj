using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Լ��� ȣ�� �ϰ� ��� � �ٸ� �Լ��� ȣ��Ǿ� �Ҷ�, �װ� �ݹ��Լ���� �θ�
public class CallBack : MonoBehaviour
{
    //������ Ư�� �Լ� ���� �Ŀ� �ٸ� �Լ��� ȣ��Ǳ� ���Ҷ� , �� �Լ���
    //C#ver:�븮�� ���·� �ѱ�.
    //Javascript ver : �Լ������ͷ� �ѱ�.
    public GameObject destoryTarget;
    public CallBackTestPopup popup;

    public Action callback;
    public void OnDestroyButtonClick()
    {
        popup.ShowPopup(OnYes);
    }
    public void OnYes(bool yes)
    {
        if (yes)
        {
            Destroy(destoryTarget);
        }
        else
        {
            print("�����ϼ˽��ϴ�.");
        }
    }

}
