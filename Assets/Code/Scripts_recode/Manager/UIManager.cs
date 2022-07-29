using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonobehaviourSingletonInterface<UIManager>
{
    public void OnGameStateChange(int oldState, int newState)
    {
        var newStateEnum=(GameState)newState;
        switch (newStateEnum)
        {
            case  GameState.Play:
                
                break;
        }
    }
}
