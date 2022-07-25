using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour,IBeattackable
{
    [SerializeField] protected StateController _stateController;
    [SerializeField] protected StackController _stackController;
    public virtual void OnBeAttack(GameObject from,int stackCount)
    {
        if (_stackController.StackCount < stackCount)
        {
            _stateController.State = (int) PlayerState.Fall;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IBeattackable>(out var ibeattack))
        {
            ibeattack.OnBeAttack(gameObject,_stackController.StackCount);
        }
    }
}
