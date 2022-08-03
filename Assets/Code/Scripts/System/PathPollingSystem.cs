using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class PathPollingSystem : MonobehaviourSingletonInterface<PathPollingSystem>
{
    [SerializeField] private GameObject _path;
    
    public ObjectPooling PathPolling;

    protected override void Awake()
    {
        base.Awake();
        PathPolling = new ObjectPooling(gameObject, _path, gameObject.transform);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnChangeScene;
    }

    private void OnChangeScene(Scene arg0, LoadSceneMode arg1)
    {
        if (arg1 == LoadSceneMode.Single)
        {
            foreach (var pool in PathPolling.Polls)
            {
                if (pool != null)
                {
                    pool.gameObject.SetActive(false);
                }
            }
        }
    }
}
