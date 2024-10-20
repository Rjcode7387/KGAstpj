using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//C# Attribute : 특정 요소(클래스, 변수 , 함수)에 대한 메타데이터를 정의
public class Myclass // 이 클래스의 값을 다른 어셈블리에서 접근하기위해서는 "직렬화"가 필요하다.
{
    public string name;
    public int id;
    public Sprite sprite;
    
}
public class ReferenceVariable : MonoBehaviour
{
   public Myclass myclass;

    public List<Myclass> myclasses;
    // Start is called before the first frame update
    void Start()
    {
        print(myclass); // print() = debug.log()
        print(myclass.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
