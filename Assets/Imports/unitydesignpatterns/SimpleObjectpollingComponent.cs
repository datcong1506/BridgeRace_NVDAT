using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectpollingComponent : MonoBehaviour
{
    public List<ObjectPooling> Value=new List<ObjectPooling>();
    public bool DestroyPoolWhenOwnerDestroy=true;


    private void Start()
    {
    }

    private void OnDestroy()
    {
        if (DestroyPoolWhenOwnerDestroy)
        {
            if (Value != null)
            {
                for (int i = 0; i < Value.Count; i++)
                {
                    if (Value[i] != null)
                    {
                        Value[i].DestroyPool();
                    }
                }
            }
        }
    }
}
