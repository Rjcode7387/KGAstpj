using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    //Transform : 기본적으로 씬에 실체를 가진 모든 GameObject들은 무조건 1개의 Transform을 가짐

    public GameObject yourObject;

    public Transform parent;

    public Transform grandParent;
    private void Start()
    {
        //모든 GameObject, Component 클래스는 transform이라는 프로퍼티로
        //해당 객체의 Transform 컴포너는를 반환
        print($"my transform : {transform}");
        print($"your transform : {yourObject.transform}");
        print($"my transform's transform : {transform.transform}");//같은거임 

        string somChildName = parent.Find("ThirdChild").GetChild(0).name;
        print(somChildName);

        parent.SetParent(grandParent,false);
        //parent.parent = grandParent; // =>똑같이 동작 하나,  일반적으로SetParent 함수를 호출함


    }
}
