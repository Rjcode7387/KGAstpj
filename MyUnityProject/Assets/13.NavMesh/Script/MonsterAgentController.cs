using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAgentController : MonoBehaviour
{
    public Transform targetPointer;
    private NavMeshAgent agent;
    public bool isstop;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(targetPointer.position);
        agent.isStopped = isstop;
    }
}
