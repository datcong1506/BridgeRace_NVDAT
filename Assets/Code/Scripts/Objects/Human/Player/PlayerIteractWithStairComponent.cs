using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIteractWithStairComponent : HumanIterractWithStairComponent
{
    [SerializeField] private CharacterController _characterController;
    public override void InvalidMove(StairStepController stepController)
    {
        var _stackCount = _stackController.StackCount;
        if (_stackCount <= 0&&stepController._owner!=gameObject)
        {
            _characterController.stepOffset = 0.05f;
        }
        else
        {
            _characterController.stepOffset = 0.3f;
        }
    }
}
