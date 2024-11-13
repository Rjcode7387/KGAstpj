using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxcontroler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� ĳ���� ��Ʈ�ѷ��� ������ �ִ��� Ȯ��
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            // ��ȣ�ۿ� ������ ���⿡ �߰�
            InteractWithPlayer();
        }
    }

    private void InteractWithPlayer()
    {
        // �÷��̾���� ��ȣ�ۿ� ���� ����
        Debug.Log("Player�� ��ȣ�ۿ�");
    }

}
