using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    //00000000 : nothing
    //00000001 : Default         1 << 0
    //00000010 : TransparentFX   1 << 1
    //00001000 : Ignore Physics  1 << 3 ������ �������� ������ 3ĭ �������� 
    //00001001 : Default , Ignore Physics : 8 + 1
    public LayerMask customMask;


    private void Start()
    {
        print($"Default Layer : {LayerMask.NameToLayer("Default")}");
        print($"TransparentFX Layer : {LayerMask.NameToLayer("TransparentFX")}");
        print($"Ignore Physics Layer : {LayerMask.NameToLayer("Ignore Physics")}");
        print($"Custom Layer Mask : {customMask.value}");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {// ScreenPointToRay : �ش� ī�޶� �������� ��ũ���� ���콺 ��ġ���� ī�޶� ���� �������� ���̸� ����.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Physics.Raycast �Լ� ȣ���, Layer Mask�� �Ķ���ͷ� ������� ������
            //�ڵ����� Ignore Raycast ���̾�� ������

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << LayerMask.NameToLayer("Ignore Raycast")))
            {
                hit.collider.GetComponentInParent<Renderer>().material.color = Color.red;
                
            }

        }


    }
}

