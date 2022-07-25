using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectBrick : MonoBehaviour
{
    [SerializeField] private StateController _stateController;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private StackController _stackController;
    public List<Transform> CurrentBricks;
    [Header("Collect Brick")] [SerializeField]
    private int MinNumBrick;
    [SerializeField] private int MaxNumBrick;
    private int TargetNumBrick;
    
    
    public StageSpawnBrickSystem CurrentStage;


    public void AddTarget(Transform newBrick)
    {

        for (int i = 0; i < CurrentBricks.Count; i++)
        {
            if (CurrentBricks[i] == newBrick)
            {
                return;
            }
        }

        if (newBrick.TryGetComponent<BrickController>(out var stat))
        {
            if (stat._owner == gameObject||stat._owner==null)
            {
                CurrentBricks.Add(newBrick);
            }
        }
    }

    public void OnAddStack()
    {
        RemoveTarget(_stackController.GetStack(_stackController.StackCount - 1).transform);
    }
    
    public void RemoveTarget(Transform brick)
    {
        for (int i = 0; i < CurrentBricks.Count; i++)
        {
            if (CurrentBricks[i] == brick)
            {
                CurrentBricks.RemoveAt(i);
                return;
            }
        }
    }


    public void OnChangeState(int oldState,int newState)
    {
        if (newState == (int) EnemyState.GotoStair)
        {
            CurrentBricks.Clear();
            var target=CurrentStage.GetRandomStair();
            _enemyMovement.MoveToTarget(target);
        }
        
    }

    private void GoToStair()
    {
        if (_stateController.State == (int) EnemyState.GotoStair)
        {
        }
    } 
    

    private void CollectBrick()
    {
        if (_stateController.State == (int) EnemyState.CollectBrick)
        {
            if (CurrentBricks != null)
            {
                if (CurrentBricks.Count > 0)
                {

                    // this can op(optimize)
                    // find nearest
                    var nearest=CurrentBricks[0];

                    for (int i = 1; i < CurrentBricks.Count; i++)
                    {
                        var currentDistance = Vector3.Magnitude(transform.position- nearest.position);
    
                        var d = Vector3.Magnitude(transform.position - CurrentBricks[i].position);
                        if (d < currentDistance)
                        {
                            nearest = CurrentBricks[i];
                        }
                    }

                    if (TargetNumBrick <= 0)
                    {

                        TargetNumBrick = UnityEngine.Random.Range(MinNumBrick, MaxNumBrick + 1);
                    }

                    if (_stackController.StackCount >= TargetNumBrick)
                    {
                        //goto stair or attack the others // randomize is good choice
                        TargetNumBrick = 0;
                        _stateController.State = (int) EnemyState.GotoStair;
                        /*
                        canChangeStateNow = true;
                    */
                    }
                    else
                    {
                        _enemyMovement.MoveToTarget(nearest.transform.position);
                    }
                }
                else
                {
                    // move to stari
                    if (_stackController.StackCount > 0)
                    {
                        // go to stair
                        _enemyMovement.MoveToTarget(CurrentStage.GetRandomStair());

                    }
                    else
                    {
                        // to a random posisison
                        _enemyMovement.MoveToTarget(CurrentStage.GetRandomPosisionOnMesh());
                        
                    }
                }
            }
        }    
    }


    private void Update()
    {
        CollectBrick();
    }

}
