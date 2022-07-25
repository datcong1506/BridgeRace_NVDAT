using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static GInput _gInput;

    public static GInput GInput
    {
        get
        {
            if (_gInput == null)
            {
                _gInput = new GInput();
            }
            return  _gInput;
        }
    }
}
