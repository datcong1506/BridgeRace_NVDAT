using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickPhysicPollingSystem : MonobehaviourSingletonInterface<BrickPhysicPollingSystem>
{
    [SerializeField] private GameObject _physsicBrick;
    
    public ObjectPooling PhyssicBrickPolling;


    protected override void Awake()
    {
        base.Awake();
        PhyssicBrickPolling = new ObjectPooling(gameObject, _physsicBrick, gameObject.transform);
        
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnChangeLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnChangeLevel;
    }

    private void OnChangeLevel(Scene arg0, LoadSceneMode arg1)
    {
        foreach (var brickPhysic in PhyssicBrickPolling.Polls)
        {
            brickPhysic.SetActive(false);
            if (brickPhysic.transform.childCount > 0)
            {
                for (int i = 0; i < brickPhysic.transform.childCount; i++)
                {
                    GameObject.Destroy(brickPhysic.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
