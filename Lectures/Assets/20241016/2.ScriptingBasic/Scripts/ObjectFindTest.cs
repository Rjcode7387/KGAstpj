using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFindTest : MonoBehaviour
{
    /*게임이 시작 되기 전 씬에서 참조 할 수 있는 오브젝트는 Inspector에서 할당하여 참조할 수 있다.*/
    public GameObject target;
    //하지만 게임이 시작되기 전 참조 할 수 없거나 , Inspector에서 값을 할당할 수 없는 객체는
    private GameObject privateTarget;
    private GameObject findedTarget;
    private GameObject newTarget;
    private GameObject namedNewTarget;
    private GameObject componentAttachedTarget;

    


    private void Start()
    {
        #region Target 오브젝트를 찾거나 추가/제거 하는 방법

        /*1. 전체 씬에서 이름으로 찾기
            이 방법은 씬에 오브젝트가 많을수록 씬 전체를 순회하며 오브젝트의 이름을 찾기에 부하가 크게 걸린다.
            같은 이름의 오브젝트가 여러개 있을경우 어떤 오브젝트가 탐색될지 확신할 수 없다.
            이런 이유로 Start함수에서만 제한적으로 쓰인다.
        */

        privateTarget = GameObject.Find("PrivateTarget");

        /*2. 전체 씬에서 특정 컴포넌트를 가지고 있는 객체를 찾는 법.
         * 컴포넌트 타입으로 찾아 게임오브젝트로 리턴하는 법
           findedTarget = FindObjectOfType(typeof(FindMe) as Component).gameObject;
         */

        findedTarget = FindObjectOfType<FindMe>().gameObject;

        print(privateTarget.name);
        print(findedTarget.name);

        /* 3. 객체를 생성 뒤 참조*/
        newTarget = new GameObject();
        namedNewTarget = new GameObject("New Target");
        componentAttachedTarget = new GameObject("Component Attached Target",typeof(FindMe),typeof(SpriteRenderer));

        /*4. 객체를 삭제
          오버로드된 함수로 딜레이를 줄수도 있다.
         */
        Destroy(privateTarget , 2f);
        #endregion



    }
}
