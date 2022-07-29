using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClearNavMeshData : MonoBehaviour
{
    private void Start()
    {
        NavMesh.RemoveAllNavMeshData();
    }
}