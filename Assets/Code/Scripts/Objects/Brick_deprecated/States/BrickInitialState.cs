using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



[RequireComponent(typeof(BrickFSM))]
public class BrickInitialState : AbStatePatternState
{

    private BrickStatComponent _brickStatComponent;

    public bool ToThisState=false;
    private void Start()
    {
        _brickStatComponent = GetComponent<BrickStatComponent>();
    }

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }


    private bool jobDone;
    public override void OnEnterState()
    {
        jobDone = false;
        canChangeStateNow = false;
        transform.rotation=Quaternion.Euler(-90,0,0);
        GetComponent<MeshRenderer>().material = GetComponent<BrickStatComponent>().Material;
        ToThisState=false;
    }

    public override void OnUpdateState()
    {
        if (jobDone)
        {
            return;
            
        }
        jobDone = true;
            
            
        transform.position=    _brickStatComponent.StageSpawnBrickSystem.GetPosisionByKeyIndex(_brickStatComponent.IndexInMesh);
        var physic=BrickPhysicPollingSystem.Singleton.PhyssicBrickPolling.Instantiate();
        physic.GetComponent<BrickPhysicController>().OnAdd(transform);
        GetComponent<BrickStatComponent>().BrickPhysicController = physic.GetComponent<BrickPhysicController>();
        canChangeStateNow = true;
        
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return ToThisState;;
    }

    public void OnDrop()
    {
        ToThisState = true;
        GetComponent<BrickStatComponent>().Owner = null;
        GetComponent<MeshRenderer>().material.SetColor("_MainColor",new Color(82,82,82));
    }
}
