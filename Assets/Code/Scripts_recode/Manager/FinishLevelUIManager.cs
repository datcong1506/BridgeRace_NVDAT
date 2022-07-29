using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelUIManager : MonobehaviourSingletonInterface<FinishLevelUIManager>
{
    [SerializeField] private GameObject _mainPanel;

    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameChangeState);
    }

    public void OnGameChangeState(int oldState, int newState)
    {
        var newStateEnum = (GameState) newState;
        switch (newStateEnum)
        {
            case GameState.End:
                _mainPanel.gameObject.SetActive(true);
                break;
            default:
                _mainPanel.gameObject.SetActive(false);
                break;
        }
    }

    
}
