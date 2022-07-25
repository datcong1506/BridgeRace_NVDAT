using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPoll : MonoBehaviour
{
    public GameObject Owner;
    public UnityEvent OnRecycle;

    private void OnEnable()
    {
        OnRecycle?.Invoke();
    }
}
