using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera _followCamera;
    [SerializeField] private Transform _camLookAt;
    [SerializeField] private Transform _camFollow;

    private StateController _stateController;
    
    private void Awake()
    {
        _stateController = GetComponent <StateController>();
    }

    public void OnChangeState(int oldState,int newState){
        if (newState == (int) PlayerState.Movement)
        {
            _followCamera.LookAt = _camLookAt;
            _followCamera.Follow = _camFollow;
        }
    }
    
    
}
