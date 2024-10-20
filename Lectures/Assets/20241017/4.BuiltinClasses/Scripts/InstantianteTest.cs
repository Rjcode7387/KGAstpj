using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantianteTest : MonoBehaviour
{
    public GameObject original;
    
    void Start()
    {
        //original 객체와 똑같이 생긴 오브젝트를 하나 더 만들어 옆에 배치하려 한다.

        /*1. 수동으로 만드는법
        GameObject clone = new GameObject("Clone");

        MeshFilter mf = clone.AddComponent<MeshFilter>();
        MeshRenderer mr = clone.AddComponent<MeshRenderer>();

        mf.mesh = original.GetComponent<MeshFilter>().mesh;
        mr.material = original.GetComponent<MeshRenderer>().material;

        clone.transform.position = original.transform.position + Vector3.right;
        */

        /*1.Instantiate로 만드는 방법
          Instantiate : 파라미터 객체를 똑같이 복제하는 함수
         */
        //Instantiate(original).transform.position = original.transform.position + Vector3.right;

        //복사와 동시에 Hierarchy 상에서 특정 객체의 자식이 되어야 할 경우
        //Instantiate(original, transform);

        //복사와 동시에 특정 위치에 특정 위치와 각도값으로 생성해야할 경우
        //Instantiate(original, Vector3.right, Quaternion.identity);

        //Instantiate 함수는 파라미터를 통해 복제된 객체를 Return한다.

        //생성되는 객체를 게임오브젝트에 리턴하여 생성되는 객체의 컴포넌트나 특정 값에 접근하여 바꿀수도 있다.
        GameObject clone = Instantiate(original, Vector3.right, Quaternion.identity);
        clone.name = "this is clone";
        //clone.GetComponent<MeshRenderer>().material.color = Color.gray;
    }
}
