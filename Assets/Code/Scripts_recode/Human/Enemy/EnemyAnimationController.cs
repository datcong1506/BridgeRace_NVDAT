using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyAnimationController : PlayerAnimationController
{
    [SerializeField]private NavMeshAgent _agent;

    private void Start()
    {
        GetComponent<StateController>().OnChangeState.AddListener(OnChangeState);
    }

    private void Update()
    {
        UpdateRealTimeParam(_agent.velocity.magnitude);
    }
}
