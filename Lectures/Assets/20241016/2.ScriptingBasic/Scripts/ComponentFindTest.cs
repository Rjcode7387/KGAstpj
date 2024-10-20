using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentFindTest : MonoBehaviour
{
    #region 타겟 게임 오브젝트는 알고 있지만 , 해당 오브젝트가 가진 특정 컴포넌트에 접근하거나 컴포넌트를 추가/제거 해야할 경우

    public GameObject target;

    private FindMe findMe;

    void Start()
    {
        /*1. target 오브젝트에서 컴포넌트를 찾으려 할때
          parameter로 컴포넌트 타입을 찾으려 할때 (박싱 언박싱 발생 type of 와 As로)
          target.GetComponent(typeof(FindMe) as Component);
        */
        findMe = target.GetComponent<FindMe>();

        print(findMe.message);

        /*2. 있는지 없는지 여부를 확인해야 할때
          out 키워드 : parameter는 기본 함수의 리턴타입에다가 out 파라미터에 있는 타입도 리턴할수있다.
        */
        bool isFound = target.TryGetComponent<BoxCollider>(out BoxCollider boxCollider);

        if (isFound) 
        {
            print($"Found Box Collider. {boxCollider}");
        }
        else 
        {
            print($"There's no BoxCollider. {boxCollider}");
        }

        /*3. Target 오브젝트의 자식 오브젝트에서 컴포넌트를 찾으려 할때
         *   GetComponentInChildren은 자식에서 찾기전에 먼저 부모에서도 찾는다.
         *   부모에서도 찾기에 자식 컴포넌트를 배열로 받아 부모 오브젝트의 컴포넌트도 출력하고
         *   자식클래스의 컴포넌트의 메세지도 출력하게끔 할수도 있다
         */
        FindMe[] children = target.GetComponentsInChildren<FindMe>();

        foreach (FindMe child in children)
        {
            print(child.message);
        }

        /*4. target 오브젝트에 컴포넌트를 추가해야할 경우*/
        FindMe newFindme = target.AddComponent<FindMe>();
        newFindme.message = "Component Added By Script";

        /*5. target 오브젝트의 컴포넌트를 제거해야할 경우
             오버로드된 함수로 딜레이를 줄수 있다.
             component.gameObject로 컴포넌트를 참조하여 해당 컴포넌트가 상속받고 있는 오브젝트 자체를 삭제도 가능하다.
         */
        Destroy(findMe.gameObject, 2f);

    }
    #endregion

}
