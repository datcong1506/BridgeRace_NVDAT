using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]protected Animator _animator;
    
    [Header("AnimParam")]
    [SerializeField] protected string ToMoveMentParam;
    [SerializeField] protected string IdleTypeParam;
    [SerializeField] protected string SpeedParam;
    [SerializeField] protected string ToFallParam;

    [SerializeField] private PlayerMovement _playerMovement;


    [SerializeField] protected int MovementState;
    [SerializeField] protected int FallState;
    [SerializeField] protected int WinState;
    private void Update()
    {
        UpdateRealTimeParam(_playerMovement.CurrentSpeed);
    }

    
    
    protected void UpdateRealTimeParam(float value)
    {
        _animator.SetFloat(SpeedParam,value);
    }


    public void OnChangeState(int oldState, int newState)
    {
        if (newState == FallState)
        {
            _animator.SetTrigger(ToFallParam);
        }

        if (newState == MovementState)
        {
            _animator.SetTrigger(ToMoveMentParam);
        }
        
        if (newState == MovementState)
        {
        }
    }

    

    public void AnimEvent_TomovementState()
    {
        GetComponent<StateController>().State = MovementState;
    }
    
    
    
    //test
    [ContextMenu("Test")]
    private void ToFall()
    {
        GetComponent<StateController>().State = FallState;
    }
}
