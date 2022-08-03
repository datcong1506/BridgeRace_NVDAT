using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenceDetechBrickController : MonoBehaviour
{
    [SerializeField] private EnemyCollectBrick _enemyTakeBrickState;
    private void OnTriggerEnter(Collider other)
    {
        var brickGroundState = other.GetComponent<BrickController>();
        if (brickGroundState != null)
        {
            _enemyTakeBrickState.AddTarget(brickGroundState.transform);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var brickGroundState = other.GetComponent<BrickController>();
        if (brickGroundState != null)
        {
            _enemyTakeBrickState.RemoveTarget(brickGroundState.transform);
            return;
        }
    }
}
