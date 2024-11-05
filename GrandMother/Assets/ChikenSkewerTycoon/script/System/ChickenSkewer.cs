using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSkewer : MonoBehaviour
{
    //대상이 필요
    //플레이어 기준 위치 설정
    //offset이란 200에 주소값을 입력후 209가 나올때 +9를 offset이라고 생각하면 좋다

    private Transform followTarget;
    private Vector3 offset = new Vector3 (0, -0.3f, 0);

    public void SetFollowTarget(Transform target)
    {
        followTarget = target;//따라다닐 대상이랑 타켓은 같다
        transform.SetParent(target);
        transform.localPosition = offset;
    }
}
