using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleMouseControl : MonoBehaviour
{
    
    //���콺 Ŀ�� ȭ�鿡 ���
    //Locked : ȭ�� �߾ӿ� ���
    //Confined : ȭ�� �׵θ� �ȿ� ����
    //None : �����
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� ���̴� ���� . Editor�� ��쿡 ���� �������ص� Esc Ű�� ������ ���콺�� ����
        Cursor.visible = false;
       
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
            
    //    }
    //}

    public static bool isFocusing;

}
