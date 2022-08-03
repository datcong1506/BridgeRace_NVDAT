using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementComponent : HumanMovementComponent
{
    private NavMeshAgent _agent;
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }

    //test
    [ContextMenu("testStop")]
    public void TestStopAgent()
    {
        _agent.isStopped = true;
    }


    public override void MoveToTarget(Vector3 target)
    {
        _agent.SetDestination(target);/**/
        /*
        _agent.SetDestination(transform.position + Vector3.forward * 2);
        */
        _agent.isStopped = false;
    }

    public override void MoveInDirec(Vector3 direc)
    {
        
    }

    public override void StopMove()
    {
        
    }
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
}
