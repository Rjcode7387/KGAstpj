using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamaTest : MonoBehaviour
{
    private void Start()
    {
        Print("첫 줄");
        Print("첫 줄", "두번째 줄");
        Print("첫 줄", "두번째 줄", "세번째 줄");

        string[] Str = { "배열 첫 줄", "배열 두번째 줄", "배열 세번째 줄" };
        Print(Str);


    }
    void Print(params string[] Str)
    {
        foreach (string str in Str)
        {
            //Debug.Log(string.Join("\n", Str));  //string.Join을 써서 디버그 하나에 다른줄로 같이 출력해보기
            Debug.Log(Str);

        }
    }
}
