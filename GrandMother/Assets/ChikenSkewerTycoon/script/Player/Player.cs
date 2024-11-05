using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("이동 관련 변수")]
    public float moveDistance = 1.0f; // 한 칸의 거리
    private Vector3 targetPosition;
    private Vector2 lastMoveDirection = Vector2.right;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    [Header("상호작용 관련 변수")]   
    public float interactionDistance = 1f;//탐지 거리
    public KeyCode interactionKey = KeyCode.F;//상호작용 키
    [Header("재화 관련 변수")]
    public float grillActivationCost = 100f; // 그릴을 살때 드는 돈
    public float dollar = 100; // 초반 소지금
    [Header("닭꼬치 관련 변수")]
    public List<GameObject> holdingChickenSkewers = new List<GameObject>();//닭꼬치를 플레이어가 들고있는 수
    public int maxHoldingChickenSkewers = 4;

   
    private SkillPanel skillPanel;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // 초기 위치 설정
        skillPanel = FindObjectOfType<SkillPanel>();
    }

    private void Update()
    {
        Move();
        GrillInterationg();
       

    }

    void Move()
    {
        // 이동할 위치가 현재 위치와 다르면 이동
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * 5);
            rb.MovePosition(newPosition);
        }
        Vector3 moveDir = Vector3.zero;
        float moveAmount = moveDistance;
        // 입력을 받으면 목표 위치 변경
        if (Input.GetKeyDown(KeyCode.W)) // 위로
        {
            moveDir = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // 아래로
        {
            moveDir = Vector3.down;
           
        }
        else if (Input.GetKeyDown(KeyCode.A)) // 왼쪽으로
        {
            sr.flipX = false;
            moveDir = Vector3.left;
            lastMoveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // 오른쪽으로
        {
            sr.flipX = true;
            moveDir = Vector3.right;
            lastMoveDirection = Vector2.right;
        }

        if (moveDir != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir,moveDistance, LayerMask.GetMask("Interaction"));
            if (hit.collider != null)
            {
               moveAmount = Mathf.Max(0,hit.distance);
            }
            targetPosition = transform.position + (moveDir * moveAmount);
        }

    }
    
    void GrillInterationg()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMoveDirection , interactionDistance, LayerMask.GetMask("Interaction"));
        Debug.DrawRay(transform.position, lastMoveDirection,Color.magenta);
       
        if (hit.collider != null)
        {
            Grill grill = hit.collider.GetComponent<Grill>();
            Counter counter = hit.collider.GetComponent<Counter>();
            SkillPanelColler skillPanelColler = hit.collider.GetComponent<SkillPanelColler>();
            if (grill != null && Input.GetKeyDown(interactionKey))
            {
                if (!grill.isPurchased && dollar >= grillActivationCost)
                {
                    dollar -= grillActivationCost;
                    grill.PurchaseGrill();
                    Debug.Log($"그릴 활성화! 현재 남은 돈: {dollar}원");
                }
                //플레이어가 홀딩할 예외
                else if (grill.isPurchased && grill.ChickenSkewers > 0 &&
                         holdingChickenSkewers.Count < maxHoldingChickenSkewers)
                {
                    grill.TakeChickenSkewers();
                }
            }
            else if (counter != null &&  Input.GetKeyDown(interactionKey))
            {
                counter.sellandreceivemoney(this);
            }
            else if (skillPanelColler != null &&  Input.GetKeyDown(interactionKey))
            {
                skillPanelColler.ToggleSkillPanel();
            }
         
        }
    
    }
   
}
