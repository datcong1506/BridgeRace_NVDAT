using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : AbStatePatternIFSM
{
    private void Update()
    {
        currentState.CheckChangeState();
        currentState.OnUpdateState();
    }


    public override ResponseEvent ChangeState(GameEventChangeStateEvent gameEventChangeStateEvent)
    {
        Transistion(gameEventChangeStateEvent.newState);
        return null;
    }


    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }
}
