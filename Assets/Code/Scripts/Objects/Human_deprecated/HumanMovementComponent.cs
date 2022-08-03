using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanMovementComponent : MonoBehaviour
{
    [SerializeField]protected float _speed;
    protected Animator _animator;

    //animparams
    [Header("AnimParam")]
    [SerializeField] protected string ToMoveMentParam;
    [SerializeField] protected string IdleTypeParam;
    [SerializeField] protected string SpeedParam;
    [SerializeField] protected string ToFallParam;


    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public abstract void MoveToTarget(Vector3 target);
    public abstract void MoveInDirec(Vector3 direc);
    public abstract void StopMove();
    
    protected void UpdateAnimParam(float speeed)
    {
        _animator.SetFloat(SpeedParam,speeed);
    }
    protected void RandomIdleType(int idelTypeCount=8)
    {
        var randomIDleIndex = UnityEngine.Random.Range(0, idelTypeCount);
        _animator.SetFloat(IdleTypeParam,(float)randomIDleIndex);
    }
}
