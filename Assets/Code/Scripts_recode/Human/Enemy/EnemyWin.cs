using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWin : PlayerWin
{
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent=GetComponent<NavMeshAgent>();
    }

    public override void Win(Transform target, int top)
    {
        Debug.Log(gameObject.name);
        _navMeshAgent.enabled=false;
        StartCoroutine(ToTarget(target));
        GetComponent<StateController>().State = (int) EnemyState.Win;

    }
}
