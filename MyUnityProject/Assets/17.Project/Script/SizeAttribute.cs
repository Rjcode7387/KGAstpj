using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�װ� ��������Ű��ƿ�

public class SizeAttribute : PropertyAttribute
{
    public Vector2 size;

    public SizeAttribute(float x, float y)
    {
        size = new Vector2(x, y);
    }
}
