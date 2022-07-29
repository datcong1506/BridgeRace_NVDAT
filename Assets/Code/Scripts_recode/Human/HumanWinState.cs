using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanWinState : MonoBehaviour
{
    protected Transform _target;

    public abstract void Ini(Transform _target, int top);
}
