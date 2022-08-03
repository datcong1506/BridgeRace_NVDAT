using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStatComponent : MonoBehaviour
{
    public Material Material;
    public GameObject Owner;
    public int IndexInMesh;

    public StageSpawnBrickSystem StageSpawnBrickSystem;
    public BrickPhysicController BrickPhysicController;
    
    private void Start()
    {
    }


}
