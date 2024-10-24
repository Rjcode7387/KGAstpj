using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{

    public float damage;
    public float moveSpeed;
    public float duration;

    public Vector3 rendererStartPos;// ��ź�� ����������, ��������Ʈ �������� ���� ��ġ.(Local Pos)

    CircleCollider2D coll;

    Transform rendererTransform;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;
        rendererTransform = transform.Find("Renderer");
    }
    private void Start()
    {
        StartCoroutine(Explosion());
    }

    //������ ��ġ���� ���� �ð� �Ŀ� ���� ���� ������ �������� �ְ� �����

    IEnumerator Explosion()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        rendererTransform.localPosition = rendererStartPos;

        while (Time.time < endTime)
        {
            yield return null;//�����Ӹ��� 1ȸ�� �ݺ�
            float currenTime = Time.time -startTime; //������Ʈ�� ������ ���� ���ӵ� �ð�
            float duration = this.duration;
            float t = currenTime / duration;

            Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);
            rendererTransform.localPosition =curRendPos;

        }

        // ����
        Collider2D[] contactedColls = Physics2D.OverlapCircleAll(transform.position, coll.radius);

        foreach (Collider2D contactedColl in contactedColls)
        {
            if (contactedColl.CompareTag("Enemy"))
            {
                print($"Exloaded Collider : {contactedColl.name}");
            }
        }
        Destroy(gameObject);
    }
}

