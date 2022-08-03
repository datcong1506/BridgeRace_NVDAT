using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadAndSingleton : MonobehaviourSingletonInterface<DontDestroyOnLoadAndSingleton>
{
    protected override void Awake()
    {
        base.Awake();
        if (DontDestroyOnLoadAndSingleton.Singleton != null &&DontDestroyOnLoadAndSingleton.Singleton!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
