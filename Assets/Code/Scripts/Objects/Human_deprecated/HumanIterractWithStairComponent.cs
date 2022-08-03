using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanIterractWithStairComponent : MonoBehaviour
{
    [SerializeField] protected StackController _stackController;
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

    public abstract void InvalidMove(StairStepController tairStepController);
}
