using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TranformTest : MonoBehaviour
{
    #region Transform : 씬에 실체를 가진 모든 GameObject들은 무조건 한개의 Transfor을 가짐
    // Start is called before the first frame update

    public GameObject yourObject;

    public Transform parent;

    public Transform grandParent;

    void Start()
    {
        //모든 GameObject, Component 클래스들은 transform 이란 프로퍼티로 해당 객체의 transform 컴포넌트를 반환
        print($"my transform : {transform}");
        print($"your transform : {yourObject.transform}");
        print($"my transform's transform : {transform.transform}");

        string someChildsName = parent.Find("ThirdChild").GetChild(0).name;
        print(someChildsName);

        parent.SetParent(grandParent, false);
        //parent.parent = grandParent //=> 똑같이 동작하나, 일반적으로 SetParent 함수를 호출함


    }

    void Update()
    {
        
    }
    #endregion
}
