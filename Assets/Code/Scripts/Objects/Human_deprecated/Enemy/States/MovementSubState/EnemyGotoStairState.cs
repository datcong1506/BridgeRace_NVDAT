using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGotoStairState : AbStatePatternState
{
    private HumanMovementComponent _humanMovementComponent;
    private HumanIterractWithStairComponent _humanIterractWithStairComponent;
    
    public Transform TargetStair;

    
    
    
    private void Start()
    {
        _humanIterractWithStairComponent = GetComponent<HumanIterractWithStairComponent>();
        _humanMovementComponent = GetComponent<HumanMovementComponent>();

    }


    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
    }

    public override void OnUpdateState()
    {
        _humanMovementComponent.MoveToTarget(TargetStair.position);
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return true;
    }

    [SerializeField]private AbStatePatternState _collectBrickState;
 
    

    private void OnTriggerEnter(Collider other)
    {
        if (abStatePatternIFSM.currentState != this)
        {
            return;
        }
        
        var stairStepController = other.GetComponent<StairStepController>();
        if (stairStepController != null)
        {
            _humanIterractWithStairComponent.OnHitStairStep(other.gameObject);
        }
    }
}
