using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonobehaviourSingletonInterface<StartUIManager>
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameChangeState);
        OnGameChangeState(-1,GameManager.Singleton.State);
    }
    
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    public void Play()
    {
        GameManager.Singleton.State = (int) GameState.Play;
    }

    private void OnGameChangeState(int oldState,int newState)
    {
        var newStateEnum = (GameState) newState;
        switch (newStateEnum)
        {
            case GameState.Play:
                _mainCanvas.enabled = false;
                break;
            case GameState.Initial:
                _mainCanvas.enabled = true;
                _inputField.text = GameData.Singleton.PlayerName;
                _levelText.text = GetCurrentSceneName();
                
                break;
        }
    }
    

    public void SetName(string newName)
    {
        GameData.Singleton.PlayerName = newName;
    }
    
}
