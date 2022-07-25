using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPhysicPollingSystem : MonobehaviourSingletonInterface<BrickPhysicPollingSystem>
{
    [SerializeField] private GameObject _physsicBrick;
    
    public ObjectPooling PhyssicBrickPolling;


    protected override void Awake()
    {
        base.Awake();
        PhyssicBrickPolling = new ObjectPooling(gameObject, _physsicBrick, gameObject.transform);

    }
}
