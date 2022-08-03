using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonobehaviourSingletonInterface<UIManager>
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnChangeLevel;
    }

    private void OnChangeLevel(Scene arg0, LoadSceneMode arg1)
    {
        
    }

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
