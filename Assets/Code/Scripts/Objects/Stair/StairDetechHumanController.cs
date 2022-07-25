using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDetechHumanController : MonoBehaviour
{
    [SerializeField]private StageSpawnBrickSystem _spawnBrickSystem;
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerIteract>(out var humanIterractWithStairComponent))
        {
            _spawnBrickSystem.ReSpawn(humanIterractWithStairComponent.gameObject);
        }
    }
}
