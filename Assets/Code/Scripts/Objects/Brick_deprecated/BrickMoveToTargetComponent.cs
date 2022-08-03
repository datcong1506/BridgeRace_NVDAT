
using System;
using System.Collections;
using Redcode.Paths;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class BrickMoveToTargetComponent : MonoBehaviour
{
    [HideInInspector]private Path _path;

    [SerializeField] private float _speed = 2f;

    // cache this tranform
    private Transform _transform;
    
    //
    private Transform stackControllerTransform;
    private Vector3 TargetLocalPosision;
    private Vector3 middleTarget;
    
    //
    private float startDistance;
    
    //
    private bool doneJob;
    
    private void Start()
    {
        _transform = transform;
    }
    
    
    // p stand for param
    public void Initital(Transform stackController,Vector3 targetLocalPosision,Vector3 p_middleTarget)
    {
        _path = PathPollingSystem.Singleton.PathPolling.Instantiate().GetComponent<Path>();
        stackControllerTransform = stackController;
        TargetLocalPosision = targetLocalPosision;
        middleTarget = p_middleTarget;
        
        
        _path.SetPoint(0,_transform.position,_transform.rotation,true);
        _path.SetPoint(1,middleTarget+Vector3.Lerp(transform.position,stackControllerTransform.position,0.5f),Quaternion.Lerp(_transform.rotation,stackControllerTransform.rotation,0.5f),true);

        startDistance = 0;

        doneJob = false;
    }
    
    public void MoveToTarget()
    {
        if(doneJob) return;
        _path.SetPoint(2,stackControllerTransform.TransformPoint(TargetLocalPosision),stackControllerTransform.rotation,true);


       startDistance += Time.deltaTime * _speed;

       if (startDistance > 1)
       {
           startDistance = 1;
           doneJob=true;
           _transform.SetParent(stackControllerTransform);
       }
       
       if (startDistance <= 1)
       {
           var point=_path.GetPointAtDistance(startDistance, true, true);
           _transform.position = point.Position;
           _transform.rotation = point.Rotation;
       }
       
    }
    
}
