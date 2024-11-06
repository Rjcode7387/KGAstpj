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
    private SkillPanelCaller skillPanelCaller;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // 초기 위치 설정
        skillPanel = GameManager.Instance.GetComponent<SkillPanel>();
        skillPanelCaller = GameManager.Instance.GetComponent<SkillPanelCaller>();
    }

    private void Update()
    {
        Move();
        CheckInteraction();
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

    void CheckInteraction()
    {
        if (!Input.GetKeyDown(interactionKey)) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMoveDirection, interactionDistance, LayerMask.GetMask("Interaction"));
        if (hit.collider == null) return;

        // 그릴 상호작용
        if (hit.collider.TryGetComponent<Grill>(out var grill))
        {
            if (!grill.isPurchased && GameManager.Instance.SpendMoney(grillActivationCost))
            {
                grill.PurchaseGrill();
            }
            else if (grill.isPurchased && grill.ChickenSkewers > 0 &&
                     GameManager.Instance.CanHoldMoreChickenSkewers())
            {
                grill.TakeChickenSkewers();
            }
            return;
        }

        // 카운터 상호작용
        if (hit.collider.TryGetComponent<Counter>(out var counter))
        {
            counter.SellChickenSkewers(this);
            return;
        }

        // 스킬패널 상호작용
        if (hit.collider.TryGetComponent<SkillPanelCaller>(out var coller))
        {
            coller.ToggleSkillPanel();
        }
    }
}
