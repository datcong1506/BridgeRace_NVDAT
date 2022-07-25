using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIteract : MonoBehaviour
{
    private StateController _stateController;
    [SerializeField] protected StackController _stackController;
    [SerializeField] private CharacterController _characterController;
    private void Start()
    {
        _stateController = GetComponent<StateController>();
    }

    //take brick
    public void Take(GameObject brick)
    {
        
    }
    
    
    public void OnHitStairStep(GameObject stairStep)
    {
        var stepController = stairStep.GetComponent<StairStepController>();
        if (stepController != null)
        {

            InvalidMove(stepController);

            if (_stackController.StackCount <= 0&&stepController._owner!=gameObject)
            {
                return;
            }

            // rm stack
            if (stepController._owner != gameObject)
            {
                _stackController.RemoveStack();
            }
            stepController.OnHitHuman(gameObject);
        }
    }

    public virtual void InvalidMove(StairStepController tairStepController)
    {
        var _stackCount = _stackController.StackCount;
        if (_stackCount <= 0&&tairStepController._owner!=gameObject)
        {
            _characterController.stepOffset = 0.05f;
        }
        else
        {
            _characterController.stepOffset = 0.3f;
        }
    }

    public virtual bool Iteract()
    {
        return _stateController.State == (int) PlayerState.Movement;
    }
    
    
    protected void OnTriggerEnter(Collider other)
    {
        if (Iteract())
        {
            if (other.TryGetComponent<IBetakeable>(out var iBetakeable))
            {
                iBetakeable.OnBetake(gameObject, _stackController);
            }

            var stairStepController = other.GetComponent<StairStepController>();
            if (stairStepController != null)
            {
                OnHitStairStep(other.gameObject);
            }
        }
    }
}