using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PathPollingSystem : MonobehaviourSingletonInterface<PathPollingSystem>
{
    [SerializeField] private GameObject _path;
    
    public ObjectPooling PathPolling;

    private void Start()
    {
        PathPolling = new ObjectPooling(gameObject, _path, gameObject.transform);
    }
}
