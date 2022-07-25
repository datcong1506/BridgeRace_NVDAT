using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIteractWithStairComponent : HumanIterractWithStairComponent
{
    private EnemyTakeBrickState _enemyCollectState;
    
    private void Start()
    {
        _enemyCollectState = GetComponent<EnemyTakeBrickState>();
    }

    public override void InvalidMove(StairStepController tairStepController)
    {
        var _stackCount = _stackController.StackCount;
        if (_stackCount <= 0&&tairStepController._owner!=gameObject)
        {
            /*
            _characterController.stepOffset = 0.05f;
        */
            GetComponent<AbStatePatternIFSM>().Transistion(_enemyCollectState);
            Debug.Log("ao that ady");
        }
        else
        {
            /*
            _characterController.stepOffset = 0.3f;
        */
        }
    }
}
