using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitialState : AbStatePatternState
{
    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
    }

    public override void OnUpdateState()
    {
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return false;
    }
}
