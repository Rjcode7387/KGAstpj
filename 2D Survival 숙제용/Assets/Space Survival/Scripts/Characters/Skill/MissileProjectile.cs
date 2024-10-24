using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{

    public float damage;
    public float moveSpeed;
    public float duration;

    public Vector3 rendererStartPos;// 폭탄이 생성됐을때, 스프라이트 랜더러가 있을 위치.(Local Pos)

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

    //생성된 위치에서 일정 시간 후에 범위 내의 적에게 데미지를 주고 사라짐

    IEnumerator Explosion()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        rendererTransform.localPosition = rendererStartPos;

        while (Time.time < endTime)
        {
            yield return null;//프레임마다 1회씩 반복
            float currenTime = Time.time -startTime; //오브젝트가 생성된 이후 지속된 시간
            float duration = this.duration;
            float t = currenTime / duration;

            Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);
            rendererTransform.localPosition =curRendPos;

        }

        // 폭발
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

