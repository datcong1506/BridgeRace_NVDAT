using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickRecycleComponent : MonoBehaviour
{
    public Transform OriginParent;
    private BrickStatComponent _brickStatComponent;
    
    
    
    private void Start()
    {
        _brickStatComponent = GetComponent<BrickStatComponent>();
        OriginParent = transform.parent;
    }

    public void Recycle()
    {
        GetComponent<AbStatePatternIFSM>().Transistion(GetComponent<AbStatePatternIFSM>().initialState);
        transform.SetParent(OriginParent);
        _brickStatComponent.StageSpawnBrickSystem.Recycle(_brickStatComponent.IndexInMesh);
        GetComponent<SphereCollider>().enabled = true;
    }
    
}
