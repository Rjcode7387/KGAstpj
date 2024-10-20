using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float PlayerSpeed;//플레이어 이동속도
    public Rigidbody ribody;
    public GameObject gameOverMessage;//게임 종료시 나오는 메세지
    public GameObject bulletprefab;//발사체 프리펩
    public Transform firepoint;//총구
    public float fireRate = 0.5f;//발사 주기
    private Coroutine fireCoroutine;

    

    private void Awake()
    {
       ribody = GetComponent<Rigidbody>();
    }
    //플레이어 상하좌우 이동및 이동 속도 구현(계속 업데이트해줘야해서 update에 넣는다 )
    void Update()
    {
        Move();
        Fire();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");//좌우
        float y = Input.GetAxis("Vertical");//상하

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(x, y, 0)*PlayerSpeed*Time.deltaTime;

        transform.position = curPos + nextPos;
    }
    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireProjectiles());
        }

        // Fire1 버튼이 떼어졌을 때
        if (Input.GetButtonUp("Fire1"))
        {
            // 발사 중지: 현재 실행 중인 코루틴을 중단합니다.
            StopCoroutine(fireCoroutine);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Enemy 태그를 가진 오브젝트와 충돌 하면 -z방향으로 튕겨남
            ribody.AddForce(Vector3.back * 20f, ForceMode.Impulse);
        }
    }
    private IEnumerator FireProjectiles()
    {
        // 무한 루프를 통해 발사체를 계속 생성
        while (true)
        {
            // 발사체 프리팹을 firePoint 위치에서 생성
            Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
            // 설정된 발사 주기 만큼 대기
            yield return new WaitForSeconds(fireRate);
        }
    }










}
