using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    #region 객체를 제거하는 4가지 방법
    public GameObject destroyTarget;
    public Component destroyComponentTarget;
    public GameObject destroyTargetWithDelay;
    public GameObject destroyTargetImmediately;
    private void Start()
    {
        /*1. GameObject를 Destroy*/
        Destroy(destroyTarget);

        /*2. Componenet를 비롯한 Object를 파괴
             Destroy 함수는 호출 이후에도 파라미터로 전달한 오브젝트에 참조가 가능하다.(객체가 아직 파괴되지 않는다.)
             Destroy 함수의 파라미터로 전달된 오브젝트는 즉시 파괴되는 것이 아닌, 
             파괴 될 리스트에 리스트업 한 후 다음 프레임이 시작되기 전 파괴된다. 따라서 해당 프레임에는 아직 객체가 존재하는것       
         */
        Destroy(destroyComponentTarget);
        FindMe findme = destroyComponentTarget as FindMe;
        print(findme.message);

        //3. 2)의 이유 때문에 , Destroy함수는 딜레이를 설정하는 것이 가능하다.
        Destroy(destroyTargetWithDelay, 3f);

        /*4. 만약, 같은 프레임이더라도 특정 객체가 즉시 파괴되기를 원한다면 DestoryImmediate() 를 사용한다.
             이 함수가 호출된 이후의 코드라인에서는 해당 객체는 참조할수 없다.
         */
        DestroyImmediate(destroyTargetImmediately);
        print(destroyTargetImmediately.name);
    }
    #endregion
}
