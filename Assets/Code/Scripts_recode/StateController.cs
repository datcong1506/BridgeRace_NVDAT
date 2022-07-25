using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    private int _state;

    public int State
    {
        get
        {
            return _state;
        }
        set
        {
            if (value != _state)
            {
                var oldState = _state;
                var newState = value;
                _state = value;
                OnChangeState?.Invoke(oldState,newState);
            }
        }
    }

    public UnityEvent<int, int> OnChangeState;
}
