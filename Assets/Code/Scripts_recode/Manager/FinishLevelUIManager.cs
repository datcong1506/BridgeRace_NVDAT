using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelUIManager : MonobehaviourSingletonInterface<FinishLevelUIManager>
{
    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;

    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameChangeState);
    }
    public void OnEnd(int playerTop)
    {
        if (playerTop == 1)
        {
            _losePanel.SetActive(false);
            _winPanel.SetActive(true);

            GameData.Singleton.CurrentLevel++;
        }
        else
        {
            _losePanel.SetActive(true);
            _winPanel.SetActive(false);
        }
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
