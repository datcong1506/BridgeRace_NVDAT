using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPollingSystem : MonobehaviourSingletonInterface<BrickPollingSystem>
{
    public GameObject Pool;

    public ObjectPooling Pooling;

    protected override void Awake()
    {
        base.Awake();
        Pooling = new ObjectPooling(gameObject, Pool,transform);
    }
}
