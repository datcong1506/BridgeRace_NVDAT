using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StairController : MonoBehaviour
{
    [SerializeField] private Transform _stairRoot;

    [SerializeField]private GameObject _stairStep;

    [SerializeField] private Vector3 _stairStepLocalOffset;

    [Header("Only For Generate")]
    [SerializeField]private float _stairStepCount; 
    [ContextMenu("GenerateStair")]
    public void GenerateStair()
    {

        for (int i = 0; i < _stairRoot.childCount; i++)
        {
            if (_stairStep != _stairRoot.GetChild(i).gameObject)
            {
                Destroy(_stairRoot.GetChild(i).gameObject);
            }
        }
        
        for (int i = 0; i < _stairStepCount; i++)
        {
            var newStairStep= Instantiate(_stairStep);
            newStairStep.transform.SetParent(_stairRoot);
            newStairStep.transform.localPosition = _stairStep.transform.localPosition + (i + 1) * _stairStepLocalOffset;
        }
    }


    private void OnEnable()
    {
        DisableRendererAllStep();
    }


    private void DisableRendererAllStep()
    {
        for (int i = 0; i < _stairRoot.childCount; i++)
        {
            _stairRoot.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    
    
    public void AddStep()
    {
        
    }

    private void Start()
    {
    }
}
