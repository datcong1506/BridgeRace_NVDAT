using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerInitialState : AbStatePatternState
{
    [SerializeField] private GameObject _inputCanvas;


    [SerializeField] private string CameraFollowName;
    [SerializeField] private Transform _camLookAt;
    [SerializeField] private Transform _camFollow;
    public override void OnEnterState()
    {
        canChangeStateNow = false;
        var cameraFollow = GameObject.Find(CameraFollowName);
        if (cameraFollow != null)
        {
            var freeLookCMN = cameraFollow.GetComponent<CinemachineVirtualCamera>();
            if (freeLookCMN != null)
            {
                freeLookCMN.Follow = _camFollow;
                freeLookCMN.LookAt = _camLookAt;
            }
        }
    }

    public override void OnUpdateState()
    {
        
    }

    public override void OnExitState()
    {
        _inputCanvas.SetActive(true);
    }

    public override bool DescistionToThisState()
    {
        return true;
    }

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }
}
