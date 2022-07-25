using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Paths;
using UnityEngine;


public class BrickGroundState : AbStatePatternState
{

    private BrickStatComponent _brickStatComponent;
    private BrickMoveToTargetComponent _brickMoveToTargetComponent;

    [SerializeField] private LayerMask _groundMask;
    private void Start()
    {
        _brickStatComponent = GetComponent<BrickStatComponent>();
        _brickMoveToTargetComponent = GetComponent<BrickMoveToTargetComponent>();
    }

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
        canChangeStateNow = false;
    }

    public override void OnUpdateState()
    {
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
        if (other.gameObject.layer == _groundMask)
        {
            _brickStatComponent.BrickPhysicController.OnRemove();
        }
    }

    public void OnBeTake(Transform stackController,Vector3 targetPosisision,Vector3 wTarget)
    {
        if(abStatePatternIFSM.currentState!=this) return;
        
        
        // set true to change to beholdState
        canChangeStateNow = true;
        
        _brickMoveToTargetComponent.Initital(stackController,targetPosisision,wTarget);
        
        
        GetComponent<SphereCollider>().enabled = false;
        
        _brickStatComponent.BrickPhysicController.OnRemove();

        /*_brickStatComponent.Rigidbody.velocity = Vector3.zero;
        _brickStatComponent.Rigidbody.useGravity = false;*/
        /*
        _brickStatComponent.Rigidbody.Sleep();
        */
        //disableRigidBody

    }
    
}
