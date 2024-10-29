using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveDistance = 1.0f; // 한 칸의 거리
    private Vector3 targetPosition;
    public float dollar;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // 초기 위치 설정
    }

    private void Update()
    {
        Move();
    }
    void Move()
    {
        // 이동할 위치가 현재 위치와 다르면 이동
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * 5);
            rb.MovePosition(newPosition);
        }

        // 입력을 받으면 목표 위치 변경
        if (Input.GetKeyDown(KeyCode.W)) // 위로
        {
            targetPosition += Vector3.up * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // 아래로
        {
            targetPosition += Vector3.down * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.A)) // 왼쪽으로
        {
            targetPosition += Vector3.left * moveDistance;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // 오른쪽으로
        {
            targetPosition += Vector3.right * moveDistance;
        }
    }
    

    
}
