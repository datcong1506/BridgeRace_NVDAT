using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Paths;
using UnityEngine;


[RequireComponent(typeof(BrickMoveToTargetComponent))]
public class BrickBeHoldState : AbStatePatternState
{
    private BrickMoveToTargetComponent _brickMoveToTargetComponent;


    private void Start()
    {
        _brickMoveToTargetComponent=GetComponent<BrickMoveToTargetComponent>();
    }

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
        var ownerMaterial = GetComponent<MeshRenderer>();

        var material = GetComponent<BrickStatComponent>().Owner.GetComponent<HumanSkinComponent>().Material;
        var color = material.GetColor("_MainColor");

        ownerMaterial.material.color = color;
    }

    public override void OnUpdateState()
    {
        _brickMoveToTargetComponent.MoveToTarget();
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return true;
    }

   
}
