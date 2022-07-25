using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement:MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;


    private void Start()
    {
        GetComponent<StateController>().OnChangeState.AddListener(OnChangeState);
    }

    public void MoveToTarget(Vector3 target)
    {
        _agent.SetDestination(target);
    }

    public void OnChangeState(int oldState, int newState)
    {
        if (newState == (int) EnemyState.Fall)
        {
            _agent.isStopped = true;
        }
    }
}
