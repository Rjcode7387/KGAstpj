using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions
{
    public static string[] Split(this string str, char Char)
    {
        return str.Split(Char);
    }

    public static string ToLower(this string str) => str.ToLower();
    public static string ToUpper(this string str) => str.ToUpper();
}

public class ExtensionTest : MonoBehaviour
{
    void Start()
    {
        string text = "¾È³ç ÇÏ¼¼¿ä";
        Debug.Log(text.Split(' ')[0]);  

        string hello = "Hello";
        Debug.Log(hello.ToUpper());     
        Debug.Log(hello.ToLower());     
    }
}
