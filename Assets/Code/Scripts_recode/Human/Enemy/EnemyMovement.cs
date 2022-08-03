using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement:MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private Vector3 lastTarget;

    private void Start()
    {
        GetComponent<StateController>().OnChangeState.AddListener(OnChangeState);
        lastTarget = transform.position;
    }
    
    
    public void MoveToTarget(Vector3 target)
    {
        if (_agent.enabled)
        {
            _agent.isStopped = false;
            _agent.SetDestination(target);
        }
    }

    public void OnChangeState(int oldState, int newState)
    {

        var newStateEnum = (EnemyState) newState;

        switch (newStateEnum)
        {
            case EnemyState.Fall:
                _agent.isStopped = true;
                break;
        }
  
    }
}
