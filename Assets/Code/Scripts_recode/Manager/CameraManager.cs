using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera InitialCam;
    public CinemachineVirtualCamera FollowPlayerCam;
    public CinemachineVirtualCamera WinCam;


    private void Start()
    {
        GameManager.Singleton.OnChangeState.AddListener(OnGameStateChange);
    }

    public void OnGameStateChange(int oldState,int newState)
    {
        var newStateEnum = (GameState) newState;
        switch (newStateEnum)
        {
            case GameState.Initial:
                InitialCam.Priority = 11;
                FollowPlayerCam.Priority = 10;
                WinCam.Priority = 9;
                break;
            case GameState.Play:
                InitialCam.Priority = 10;
                FollowPlayerCam.Priority = 11;
                WinCam.Priority = 9;
                break;
            case GameState.End:
                InitialCam.Priority = 9;
                FollowPlayerCam.Priority = 10;
                WinCam.Priority = 11;
                break;
        }
    }
    
    public void Initial()
    {
        
    }
    
}
