using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
/*
using Unity.AI.Navigation;
*/
using UnityEngine;
using UnityEngine.Experimental.AI;

using UnityEngine;
using UnityEngine.AI;
public class StairStepController : MonoBehaviour
{
    public GameObject _owner;

    
    public void OnChangeOwner(GameObject newOwner)
    {
        _owner = newOwner;
        GetComponent<MeshRenderer>().material.color =
            newOwner.GetComponent<HumanSkinComponent>().Material.GetColor("_MainColor");
        

    }

    public void OnHitHuman(GameObject newOwner)
    {
        if(_owner==gameObject) return;
        
        if (_owner == null)
        {
            GetComponent<MeshRenderer>().enabled=true;
            OnChangeOwner(newOwner);
        }
        else
        {
            OnChangeOwner(newOwner);
        }
        
    }

    
}
