using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewStageDetech : MonoBehaviour
{
    [SerializeField]private StageSpawnBrickSystem Stage;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.TryGetComponent<EnemyController>(out var controller)))
        {
            var collectBrick=other.GetComponent<EnemyCollectBrick>();
            collectBrick.OnEnterNewStage(Stage);
        }

        if (other.TryGetComponent<PlayerIteract>( out var iteract))
        {
            
        }
    }

   
}
