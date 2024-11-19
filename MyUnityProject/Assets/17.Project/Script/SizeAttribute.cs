using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//그건 상관없을거같아여

public class SizeAttribute : PropertyAttribute
{
    public Vector2 size;

    public SizeAttribute(float x, float y)
    {
        size = new Vector2(x, y);
    }
}
