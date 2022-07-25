using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerIteractBrickComponent))]
[RequireComponent(typeof(PlayerMovementComponent))]
[RequireComponent(typeof(HumanIterractWithStairComponent))]
public class PlayerMovementState : AbStatePatternState
{
    private HumanMovementComponent _playerMovementComponent;
    private HumanIteractWithBrickComponent _playerIteractBrickComponent;
    private HumanIterractWithStairComponent _humanIterractWithStairComponent;
    private void Start()
    {
        _playerMovementComponent = GetComponent<HumanMovementComponent>();
        _playerIteractBrickComponent = GetComponent<HumanIteractWithBrickComponent>();
        _humanIterractWithStairComponent = GetComponent<HumanIterractWithStairComponent>();
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
       //movement Handle
       var moveDirec = PlayerInputSystem.Singleton.direc;
       if (moveDirec != Vector2.zero)
       {
           _playerMovementComponent.MoveInDirec(new Vector3(moveDirec.x,0,moveDirec.y));
       }
       else
       {
           _playerMovementComponent.StopMove();
       }
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (abStatePatternIFSM.currentState != this)
        {
            return;
        }

        // iterract with brick handle
        var brickGroundState = other.GetComponent<BrickGroundState>();
        if (brickGroundState != null)
        {

            _playerIteractBrickComponent.TryTakeBrick(other.gameObject);
            return;
        }

        var stairStepController = other.GetComponent<StairStepController>();
        if (stairStepController != null)
        {
            _humanIterractWithStairComponent.OnHitStairStep(other.gameObject);
        }


    }
}
