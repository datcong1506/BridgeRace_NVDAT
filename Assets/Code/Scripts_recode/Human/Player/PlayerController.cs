using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState:int
{
    Initial=0,
    Movement=1,
    Fall=2,
    Win=3,
}

public class PlayerController : StateController
{
    
    [SerializeField]
    private void Awake()
    {
        State = (int) PlayerState.Movement;
    }
    
    
}
