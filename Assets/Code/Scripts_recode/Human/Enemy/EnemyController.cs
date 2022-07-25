using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyState
{
    Initial=0,
    CollectBrick=1,
    GotoStair=2,
    Attack=3,
    Fall=4,
    Win=5,
}
public class EnemyController : StateController
{
    private void Awake()
    {
        State = (int) EnemyState.CollectBrick;
    }
    
}
