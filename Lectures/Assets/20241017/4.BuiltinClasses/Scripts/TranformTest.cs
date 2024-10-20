using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TranformTest : MonoBehaviour
{
    #region Transform : ���� ��ü�� ���� ��� GameObject���� ������ �Ѱ��� Transfor�� ����
    // Start is called before the first frame update

    public GameObject yourObject;

    public Transform parent;

    public Transform grandParent;

    void Start()
    {
        //��� GameObject, Component Ŭ�������� transform �̶� ������Ƽ�� �ش� ��ü�� transform ������Ʈ�� ��ȯ
        print($"my transform : {transform}");
        print($"your transform : {yourObject.transform}");
        print($"my transform's transform : {transform.transform}");

        string someChildsName = parent.Find("ThirdChild").GetChild(0).name;
        print(someChildsName);

        parent.SetParent(grandParent, false);
        //parent.parent = grandParent //=> �Ȱ��� �����ϳ�, �Ϲ������� SetParent �Լ��� ȣ����


    }

    void Update()
    {
        
    }
    #endregion
}
