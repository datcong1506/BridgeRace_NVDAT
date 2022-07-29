using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIManager : MonobehaviourSingletonInterface<PauseUIManager>
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameStateChange);
    }

    public void Pause()
    {
        GameManager.Singleton.State = (int) GameState.Pause;
    }

    public void Resume()
    {
        GameManager.Singleton.State = (int) GameState.Play;
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGameStateChange(int oldState,int newstate)
    {
        var newStateEnum = (GameState) newstate;
        switch (newStateEnum)
        {   
            case GameState.Pause:
                _pauseButton.SetActive(false);
                _pauseMenu.SetActive(true);
                break;
            case GameState.Play:
                _pauseButton.SetActive(true);
                _pauseMenu.SetActive(false);
                break;
            default:
                _pauseButton.SetActive(false);
                _pauseMenu.SetActive(false);
                break;
        }
    }
}
