using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUIInteraction : MonoBehaviour
{
    //Button ������Ʈ�� OnClick() �̺�Ʈ�� �Ҵ��Ͽ� �ش� ��ư�� Ŭ�� �� ������ ȣ�� �ǵ���
    //"����Ƽ ������"Inpector�� �Ҵ�� ��� �������� �������ش�
    //Inspector���� �ش� �Լ��� �����Ϸ��� ���� �����ڰ� public�̾�� �Ѵ�.
    public void ButtonClick()//�� �Լ��� ��ư�� Ŭ�� �ɶ� ȣ��
    {
        print("��ưŬ��");
    }
    public void ButtonClickWithParam(string param)
    {
        print($"��ư Ŭ����. �Ķ���� : {param}");
    }
    public void ButtonClickFloatWithParam(float param)
    {
        print($"��ư Ŭ����. �Ķ���� : {param}");
    }
    public void ButtonClickBoolWithParam(bool param)
    {
        print($"��ư Ŭ����. �Ķ���� : {param}");
    }

}
