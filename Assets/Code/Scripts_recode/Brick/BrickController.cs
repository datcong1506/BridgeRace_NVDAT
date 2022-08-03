using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Paths;
using Unity.VisualScripting;
using UnityEngine;

public class BrickController : MonoBehaviour,IBetakeable
{
    private Transform _transform;
    public GameObject _owner;
    private Material _material;
    [SerializeField] private Color _dropColor;
    public Material Material
    {
        get
        {
            return _material;
        }
        set
        {
            _material = value;
            GetComponent<MeshRenderer>().sharedMaterial = _material;
        }
    }
    public StageSpawnBrickSystem _stageSpawnBrickSystem;
    public int keyIndexInMesh;
    
    
    private Path _path;
    [SerializeField] private float _speed = 2f;
    private Transform stackControllerTransform;
    private Vector3 TargetLocalPosision;
    private Vector3 middleTarget;
    private float startDistance;
    private bool doneJob=true;




   
    private void OnDisable()
    {
        doneJob = true;
    }

    public void SetTarget(Transform stackController,Vector3 targetLocalPosision,Vector3 p_middleTarget)
    {
        _path = PathPollingSystem.Singleton.PathPolling.Instantiate().GetComponent<Path>();
        stackControllerTransform = stackController;
        TargetLocalPosision = targetLocalPosision;
        middleTarget = p_middleTarget;
        
        transform.SetParent(stackController);
        
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
            _path.gameObject.SetActive(false);
        }
        if (startDistance <= 1)
        {
            var point=_path.GetPointAtDistance(startDistance, true, true);
            _transform.position = point.Position;
            _transform.rotation = point.Rotation;
        }
    }
    private void Start()
    {
        _transform = transform;
    }
    
    private void LateUpdate()
    {
        MoveToTarget();
    }

    private void OnEnable()
    {
        StartCoroutine(Delay(OnSpawn));
    }
    
    public void OnBetake(GameObject from,StackController stackController)
    {
        if (_owner == from||_owner==null)
        {
            var parent = transform.parent;
            transform.SetParent(null);
            parent.gameObject.SetActive(false);
            _owner=from;
            SetTarget(stackController.transform,stackController.GetNextLPosision(),Vector3.zero);
            GetComponent<SphereCollider>().enabled = false;
            stackController.AddStack(gameObject);
            Material.SetColor("_MainColor", _owner.GetComponent<HumanSkinComponent>().Material.GetColor("_MainColor"));
            if (keyIndexInMesh != -1)
            {
                _stageSpawnBrickSystem.Recycle(keyIndexInMesh);
            }
            keyIndexInMesh = -1;
        }
    }
    
    public void OnDrop(){
        transform.SetParent(null);
        var brickPhysicContain=BrickPhysicPollingSystem.Singleton.PhyssicBrickPolling.Instantiate(transform.position,transform.rotation);
        var brickPhysicController =brickPhysicContain.GetComponent<BrickPhysicController>();
        brickPhysicController.OnAddPhysic(transform);
        Material.SetColor("_MainColor",_dropColor);
        _owner = null;
        
    }

    public void OnGrounded()
    {
        GetComponent<SphereCollider>().enabled = true;
    }
   
    public void Recycle(){
        GetComponent<SphereCollider>().enabled = false;
        transform.position = Vector3.down * 100;
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
    
    public void Initial(StageSpawnBrickSystem stageSpawnBrickSystem,GameObject owner,Material material)
    {
        _stageSpawnBrickSystem = stageSpawnBrickSystem;
        _owner = owner;
        Material = material;
    }

    private void OnSpawn()
    {
        if (_stageSpawnBrickSystem != null)
        {
            transform.rotation=Quaternion.Euler(-90,0,0);
            transform.position = _stageSpawnBrickSystem.GetPosisionByKeyIndex(keyIndexInMesh);
            var brickPhysicContain=BrickPhysicPollingSystem.Singleton.PhyssicBrickPolling.Instantiate(transform.position,transform.rotation);
            var brickPhysicController =brickPhysicContain.GetComponent<BrickPhysicController>();
            brickPhysicController.OnAdd(transform);
            Material.SetColor("_MainColor", _owner.GetComponent<HumanSkinComponent>().Material.GetColor("_MainColor"));
        }
    }
    
    IEnumerator Delay(Action f)
    {
        yield return new WaitForNextFrameUnit();
        f();
    }    
}
