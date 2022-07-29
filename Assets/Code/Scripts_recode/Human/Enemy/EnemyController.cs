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
        State = (int) EnemyState.Initial;

    }

    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameChangeState);
    }


    public void OnGameChangeState(int oldState, int newState)
    {
        var newStateEnum = (GameState) newState;
        switch (newStateEnum)
        {
            case GameState.Play:
                if (State == (int) EnemyState.Initial)
                {
                    State = (int) EnemyState.CollectBrick;
                }
                break;
        }
    }



}
