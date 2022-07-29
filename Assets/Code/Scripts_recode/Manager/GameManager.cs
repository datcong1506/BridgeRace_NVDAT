using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public enum GameState
{
    Initial,
    Play,
    Pause,
    End
}

public class GameManager : MonobehaviourSingletonInterface<GameManager>
{
    [SerializeField]private int _state;
    public int State
    {
        get
        {
            return _state;
        }
        set
        {
            if (value != _state)
            {
                var oldState = _state;
                var newState = value;
                _state = value;
                OnChangeState?.Invoke(oldState,newState);
            }
        }
    }
    public UnityEvent<int, int> OnChangeState;
    
    
    
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnChangeState.AddListener(OnGameChangeState);
    }

    private void Recycle()
    {
        State = (int) GameState.Initial;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    private void OnGameChangeState(int oldState,int newState)
    {
        var newStateEnum = (GameState) newState;
        switch (newStateEnum)
        {
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            default:
                Time.timeScale = 1f;
                break;
        }
    }
    
    
    
}
