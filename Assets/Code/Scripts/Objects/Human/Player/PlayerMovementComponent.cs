using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementComponent : HumanMovementComponent
{
    // movement engine
    private CharacterController _characterController;

    //
    [Header("SmoothRotate")] [SerializeField]
    private float _smoothRoatateTime;
    private float _angularVelocity;


    [Header("Gravity Simulate")] [SerializeField]
    private LayerMask _walkAbleLayer;
    [SerializeField][Range(0,1)] private float _radiusGroundCheck;
    [SerializeField] private Transform _foot;
    [SerializeField] private float _gravityValue = -9.81f;
    private Vector3 _fallVelocity;
    private bool isGround;
    
    protected virtual void Start()
    {
        // set _charactercontroller
        _characterController = GetComponent<CharacterController>();
        //
        _animator = GetComponent<Animator>();
        
        
    }

    private void Update()
    {
        SimulateGravity();
    }

    private void SimulateGravity()
    {
        var groundDetech = Physics.CheckSphere(_foot.position, _radiusGroundCheck, _walkAbleLayer);
        var isFalling = _fallVelocity.y <= 0 ? true : false;

        if (groundDetech && isFalling)
        {
            _fallVelocity.y = -1f;
        }
        else
        {
            _fallVelocity.y += Time.deltaTime * _gravityValue;
        }
        _characterController.Move(_fallVelocity * Time.deltaTime);
    }
    
    public override void MoveToTarget(Vector3 target)
    {
    }

    public override void MoveInDirec(Vector3 direc)
    {
        direc.Normalize();


      

        var targetAngle = Mathf.Atan2(direc.x, direc.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _angularVelocity,
            _smoothRoatateTime);
        transform.rotation=Quaternion.Euler(0,angle,0);
        var currentVelocityValue = Mathf.Lerp(Vector3.Magnitude(_characterController.velocity),_speed,0.2f);
        _characterController.Move(direc * currentVelocityValue * Time.deltaTime);
        UpdateAnimParam(currentVelocityValue);
    }

    public override void StopMove()
    {
        
        var currentVelocityValue = Mathf.Lerp(0,Vector3.Magnitude(_characterController.velocity),0.8f);
        _characterController.Move(_characterController.velocity.normalized * currentVelocityValue * Time.deltaTime);
        UpdateAnimParam(currentVelocityValue);
        
    }

   
    
}
