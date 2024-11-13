using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//상태 열거
public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Return
}

public class fsm : MonoBehaviour
{
    private EnemyState currentState;
    private NavMeshAgent agent;
    public Transform player;
    public float detectRange = 10f;
    public float attackRange = 2f;
    private Vector3 originPos;
    private Vector3 randomPatrolPos;
    public float patrolRange = 5f;

    private float stateTimer;
    private float idleTime = 3f;
    private float attackDelay = 1f;
    private float lastAttackTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Idle;
        originPos = transform.position;
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;
            case EnemyState.Patrol:
                UpdatePatrolState();
                break;
            case EnemyState.Chase:
                UpdateChaseState();
                break;
            case EnemyState.Attack:
                UpdateAttackState();
                break;
            case EnemyState.Return:
                UpdateReturnState();
                break;
        }
    }
    private void UpdateIdleState()
    {
        stateTimer += Time.deltaTime;

        if (IsPlayerInRange(detectRange))
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (stateTimer >= idleTime)
        {
            SetPatrolDestination();
            currentState = EnemyState.Patrol;
            stateTimer = 0f;
        }
    }
    private void UpdatePatrolState()
    {
        if (IsPlayerInRange(detectRange))
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (Vector3.Distance(transform.position, randomPatrolPos) < 0.1f)
        {
            currentState = EnemyState.Idle;
            stateTimer = 0f;
        }
    }
    private void UpdateChaseState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        agent.SetDestination(player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer > detectRange)
        {
            currentState = EnemyState.Return;
        }
    }
    private void UpdateAttackState()
    {
        agent.SetDestination(transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (Time.time - lastAttackTime >= attackDelay)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }
    private void UpdateReturnState()
    {
        agent.SetDestination(originPos);

        if (Vector3.Distance(transform.position, originPos) < 0.1f)
        {
            currentState = EnemyState.Idle;
            stateTimer = 0f;
        }
        else if (IsPlayerInRange(detectRange))
        {
            currentState = EnemyState.Chase;
        }
    }

    private void SetPatrolDestination()
    {
        randomPatrolPos = originPos + Random.insideUnitSphere * patrolRange;
        randomPatrolPos.y = transform.position.y;
        agent.SetDestination(randomPatrolPos);
    }

    private void PerformAttack()
    {
        Debug.Log("공격!");
        // 여기에 실제 공격 로직 구현
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.position) <= range;
    }
}








