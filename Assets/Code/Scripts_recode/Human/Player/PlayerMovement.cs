using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
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


    
    private StateController _stateController;

    [SerializeField]private float _speed;
    
    public float CurrentSpeed { get; private set; }
    
    private void Start()
    {
        _stateController = GetComponent < StateController>();
        _characterController = GetComponent<CharacterController>();
    }
    
    
    private void Update()
    {
        if (_stateController.State == (int) (PlayerState.Movement))
        {
            //
            var direcInput = PlayerInputSystem.Singleton.direc;
            if (direcInput.magnitude <= 0.1f)
            {
                StopMove();
            }
            else
            {
                MoveInDirec(new Vector3(direcInput.x,0,direcInput.y));
            }
            
        }

        SimulateGravity();
    }
    private void MoveInDirec(Vector3 direc)
    {
        direc.Normalize();
        var targetAngle = Mathf.Atan2(direc.x, direc.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _angularVelocity,
            _smoothRoatateTime);
        transform.rotation=Quaternion.Euler(0,angle,0);
         CurrentSpeed = Mathf.Lerp(Vector3.Magnitude(_characterController.velocity),_speed,0.2f);
        _characterController.Move(direc * CurrentSpeed * Time.deltaTime);
    }
    private void StopMove()
    {
        
        CurrentSpeed = Mathf.Lerp(0,Vector3.Magnitude(_characterController.velocity),0.8f);
        _characterController.Move(_characterController.velocity.normalized * CurrentSpeed * Time.deltaTime);
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
}
