using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanFallState : AbStatePatternState
{
    private bool toThisState = false;


    public  StackController _stackController;
    
    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }
    
    
    
    public override void OnEnterState()
    {
        toThisState = false;
        _stackController.Drop();
    }

    public override void OnUpdateState()
    {
    }

    public override void OnExitState()
    {
    }
    
    public override bool DescistionToThisState()
    {
        return toThisState;
    }


    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HumanFallState>(out var _humanFallState))
        {

            var stackCount1=_stackController.StackCount;
            var stackCount2=_humanFallState._stackController.StackCount;


            if (stackCount1 < stackCount2)
            {
                toThisState = true;
            }
        }    
    }
}
