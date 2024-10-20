using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float PlayerSpeed;//�÷��̾� �̵��ӵ�
    public Rigidbody ribody;
    public GameObject gameOverMessage;//���� ����� ������ �޼���
    public GameObject bulletprefab;//�߻�ü ������
    public Transform firepoint;//�ѱ�
    public float fireRate = 0.5f;//�߻� �ֱ�
    private Coroutine fireCoroutine;

    

    private void Awake()
    {
       ribody = GetComponent<Rigidbody>();
    }
    //�÷��̾� �����¿� �̵��� �̵� �ӵ� ����(��� ������Ʈ������ؼ� update�� �ִ´� )
    void Update()
    {
        Move();
        Fire();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");//�¿�
        float y = Input.GetAxis("Vertical");//����

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

        // Fire1 ��ư�� �������� ��
        if (Input.GetButtonUp("Fire1"))
        {
            // �߻� ����: ���� ���� ���� �ڷ�ƾ�� �ߴ��մϴ�.
            StopCoroutine(fireCoroutine);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Enemy �±׸� ���� ������Ʈ�� �浹 �ϸ� -z�������� ƨ�ܳ�
            ribody.AddForce(Vector3.back * 20f, ForceMode.Impulse);
        }
    }
    private IEnumerator FireProjectiles()
    {
        // ���� ������ ���� �߻�ü�� ��� ����
        while (true)
        {
            // �߻�ü �������� firePoint ��ġ���� ����
            Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
            // ������ �߻� �ֱ� ��ŭ ���
            yield return new WaitForSeconds(fireRate);
        }
    }










}
