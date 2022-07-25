using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem : MonobehaviourSingletonInterface<LevelSystem>
{
    public GameObject[] Enemy;


    private GameState _gameState;
    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            if (value != _gameState)
            {
                _gameState = value;
                OnChangeGameStateEvent?.Invoke(_gameState);
            }
        }
    }

    public UnityEvent<GameState> OnChangeGameStateEvent;
}


public enum GameState
{
    Initial,// Menu
    Playing,
    Pause,
}