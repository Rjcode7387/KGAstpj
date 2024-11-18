using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelegatePj : MonoBehaviour
{
    public InputField input1;
    public InputField input2;
    public Text resultText;

    private delegate float CalcOperation(float a, float b);
    private CalcOperation current;

    private float Add(float a, float b) => a + b;
    private float Subtract(float a, float b) => a - b;     
    private float Multiply(float a, float b) => a * b;     
    private float Divide(float a, float b) => b != 0 ? a / b : float.NaN;

    public void OnPlusButtonClick() => current = Add;        
    public void OnMinusButtonClick() => current = Subtract;  
    public void OnMultiplyButtonClick() => current = Multiply;  
    public void OnDivideButtonClick() => current = Divide;


    public void Calc()
    {
        
        if (float.TryParse(input1.text, out float num1) &&
            float.TryParse(input2.text, out float num2))
        {
           
            float result = current.Invoke(num1, num2);
           
            resultText.text = result.ToString();
        }
       
    }

}
