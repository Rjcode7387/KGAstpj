using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//상태 열거
public enum EnemyState
{
    Idle,
    Petrol,
    Chase,
    Attack,
    Return
}

public class fsm : MonoBehaviour
{
    private EnemyState enemystate;
    private NavMeshAgent agent;
    public Transform player;
    public float detectRange = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemystate = EnemyState.Idle;
    }






}

