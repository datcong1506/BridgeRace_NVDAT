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

    private MeshRenderer _meshRenderer;


    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }


    public void OnChangeOwner(GameObject newOwner)
    {
        _owner = newOwner;
        /*GetComponent<MeshRenderer>().material.color =
            newOwner.GetComponent<HumanSkinComponent>().Material.GetColor("_MainColor");*/
        StartCoroutine(ChangeColor(newOwner.GetComponent<HumanSkinComponent>().Material.GetColor("_MainColor")));

    }

    IEnumerator ChangeColor(Color targetColor)
    {
        var firstColor = _meshRenderer.material.color;
        var t = 0;
        do
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color,targetColor,0.1f);
            t++;
            if (t > 30) break;
            yield return null;
        } while (true);
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
