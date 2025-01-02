using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTest : MonoBehaviour
{
    public void print(string x) => MonoBehaviour.print(x as object);
    public void OnSelect()
    {
        print("µø¿€«‘");
    }
}
