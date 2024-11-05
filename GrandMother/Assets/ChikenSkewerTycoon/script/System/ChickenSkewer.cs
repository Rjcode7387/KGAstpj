using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSkewer : MonoBehaviour
{
    //����� �ʿ�
    //�÷��̾� ���� ��ġ ����
    //offset�̶� 200�� �ּҰ��� �Է��� 209�� ���ö� +9�� offset�̶�� �����ϸ� ����

    private Transform followTarget;
    private Vector3 offset = new Vector3 (0, -0.3f, 0);

    public void SetFollowTarget(Transform target)
    {
        followTarget = target;//����ٴ� ����̶� Ÿ���� ����
        transform.SetParent(target);
        transform.localPosition = offset;
    }
}
