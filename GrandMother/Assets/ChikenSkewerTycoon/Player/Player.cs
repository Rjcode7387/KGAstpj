using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveDistance = 1.0f; // �� ĭ�� �Ÿ�
    private Vector3 targetPosition;
    public float dollar;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // �ʱ� ��ġ ����
    }

    private void Update()
    {
        Move();
    }
    void Move()
    {
        // �̵��� ��ġ�� ���� ��ġ�� �ٸ��� �̵�
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * 5);
            rb.MovePosition(newPosition);
        }

        // �Է��� ������ ��ǥ ��ġ ����
        if (Input.GetKeyDown(KeyCode.W)) // ����
        {
            targetPosition += Vector3.up * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // �Ʒ���
        {
            targetPosition += Vector3.down * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.A)) // ��������
        {
            targetPosition += Vector3.left * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // ����������
        {
            targetPosition += Vector3.right * moveDistance;
        }
    }
    

    
}
