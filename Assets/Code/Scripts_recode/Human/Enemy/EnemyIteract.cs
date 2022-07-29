using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIteract : PlayerIteract
{
    [SerializeField] private EnemyCollectBrick _enemyCollectBrick;
    public override bool Iteract()
    {
        return _stateController.State is (int) EnemyState.Attack or (int) EnemyState.CollectBrick or (int) EnemyState.GotoStair;
    }

    public override void InvalidMove(StairStepController tairStepController)
    {
        var _stackCount = _stackController.StackCount;
        if (_stackCount <= 0&&tairStepController._owner!=gameObject)
        {
            /*
            _characterController.stepOffset = 0.05f;
        */
            _stateController.State = (int) EnemyState.CollectBrick;
        }
        else
        {
            /*
            _characterController.stepOffset = 0.3f;
        */
        }
    }
}
