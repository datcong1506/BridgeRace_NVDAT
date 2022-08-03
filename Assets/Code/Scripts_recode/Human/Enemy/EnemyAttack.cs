using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : PlayerAttack
{
    public override void OnBeAttack(GameObject @from, int stackCount)
    {
        if (_stackController.StackCount < stackCount)
        {
            _stateController.State = (int) EnemyState.Fall;
        }
    }
}
