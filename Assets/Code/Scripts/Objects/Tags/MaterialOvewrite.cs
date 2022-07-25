using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialOvewrite : MonoBehaviour
{

    [SerializeField] private Meshtype _target=Meshtype.MeshRenderer;
    // Start is called before the first frame update
    private void Start()
    {
        switch (_target)
        {
            case Meshtype.MeshRenderer:
                var meshRender = GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRender.materials.Length; i++)
                {
                    meshRender.materials[i] = new Material(meshRender.materials[i]);
                }
                break;
        }
    }
}

public enum Meshtype
{
    MeshRenderer,
}