using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiteralVariableTest : MonoBehaviour
{
    private int intVar = 1;
    public float floatVaR = 0.1f;
    public bool boolVar = true;

    public short shortVar = short.MinValue;
    public ushort ushortVar = ushort.MaxValue;

    public int uintVar;
    public long longVar;
    public ulong ulongVar;
    public double doubleVar;
    public decimal decimalVar = decimal.MaxValue;

    public char charVar = 'A';
    public string strVar = null;


    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(strVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void Reset()
    //{
    //    strVar = "Unity";
    //}
}
