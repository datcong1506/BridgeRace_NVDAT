using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    [SerializeField] private GameObject _detechBrick;
    [SerializeField]private StageSpawnBrickSystem CurrentStage;
    private Vector3 stairPosision;
    [SerializeField] private Vector3 targetStair;


    private void Start()
    {
        targetStair = CurrentStage.GetRandomStair();
    }


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


    public void OnEnterNewStage(StageSpawnBrickSystem stageSpawnBrickSystem)
    {
        CurrentStage = stageSpawnBrickSystem;
        _stateController.State = (int) (EnemyState.CollectBrick);
        targetStair = CurrentStage.GetRandomStair();
    }

    public void OnChangeState(int oldState,int newState)
    {
        var nState = (EnemyState) newState;

        switch (nState)
        {
            case EnemyState.GotoStair:
                CurrentBricks.Clear();
                _enemyMovement.MoveToTarget(targetStair);
                _detechBrick.gameObject.SetActive(false);
                break;
            case EnemyState.Fall:
                _stackController.Drop();
                break;
            case EnemyState.CollectBrick:
                _detechBrick.gameObject.SetActive(true);
                break;
            default:
                break;
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
                    
                    var directodis = nearest.position - transform.position;
                    directodis.y = 0;
                    if (directodis.magnitude < 0.1f)
                    {
                        RemoveTarget(nearest);
                    }

                    if (nearest.TryGetComponent<BrickController>(out var controller))
                    {
                        if (controller._owner != null && controller._owner != gameObject)
                        {
                            RemoveTarget(controller.transform);
                            return;
                        }
                    }
                    
                    if (TargetNumBrick <= 0)
                    {

                        TargetNumBrick = UnityEngine.Random.Range(MinNumBrick, MaxNumBrick + 1);
                    }

                    if (_stackController.StackCount >= TargetNumBrick)
                    {
                        TargetNumBrick = 0;
                        _stateController.State = (int) EnemyState.GotoStair;
                    }
                    else
                    {
                        _enemyMovement.MoveToTarget(nearest.transform.position);
                    }
                }
                else
                {
                    if (_stackController.StackCount > 0)
                    {
                        _stateController.State = (int) EnemyState.GotoStair;
                    }
                    else
                    {
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
