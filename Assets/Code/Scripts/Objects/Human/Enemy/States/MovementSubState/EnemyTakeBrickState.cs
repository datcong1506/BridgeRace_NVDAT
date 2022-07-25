using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeBrickState : AbStatePatternState
{
    private HumanMovementComponent _humanMovementComponent;
    private HumanIteractWithBrickComponent _humanIteractBrickComponent;

    private Transform _transform;
    
    
    [SerializeField] private StackController _stackController;
    public List<Transform> CurrentBricks;
    [Header("Collect Brick")] [SerializeField]
    private int MinNumBrick;
    [SerializeField] private int MaxNumBrick;
    private int TargetNumBrick;
    
    
    public GameObject CurrentStage;
    private void Start()
    {
        _humanMovementComponent = GetComponent<HumanMovementComponent>();
        _humanIteractBrickComponent = GetComponent<HumanIteractWithBrickComponent>();

        CurrentBricks = new List<Transform>();

        _transform = transform;
    }
    
    
    

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
        canChangeStateNow = false;
        _humanMovementComponent.MoveToTarget(CurrentStage.transform.position);
    }

    public override void OnUpdateState()
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
                    var currentDistance = Vector3.Magnitude(_transform.position- nearest.position);

                    var d = Vector3.Magnitude(_transform.position - CurrentBricks[i].position);
                    if (d < currentDistance)
                    {
                        nearest = CurrentBricks[i];
                    }
                }
                
                
                
                
                switch (nearest.GetComponent<AbStatePatternIFSM>().currentState)
                {   
                    case BrickGroundState _:

                        if (TargetNumBrick <= 0)
                        {

                            TargetNumBrick = UnityEngine.Random.Range(MinNumBrick, MaxNumBrick + 1);
                        }

                        
                        if (_stackController.StackCount >= TargetNumBrick)
                        {

                            //goto stair or attack the others // randomize is good choice
                            TargetNumBrick = 0;
                            canChangeStateNow = true;
                        }
                        else
                        {
                            _humanMovementComponent.MoveToTarget(nearest.transform.position);
                        }

                        break;
                }
            }
            else
            {
                // move to stari
                if (_stackController.StackCount > 0)
                {
                    canChangeStateNow = true;

                }
                else
                {
                    
                }
            }
        }
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return true;
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

        if (newBrick.TryGetComponent<BrickStatComponent>(out var stat))
        {
            if (stat.Owner == gameObject)
            {
                CurrentBricks.Add(newBrick);
            }
        }
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


    private void OnTriggerEnter(Collider other)
    {
        var s = abStatePatternIFSM.currentState as EnemyGotoStairState;
        if (abStatePatternIFSM.currentState != this && s!=null)
        {
            return;
        }
        

        // iterract with brick handle
        var brickGroundState = other.GetComponent<BrickGroundState>();
        if (brickGroundState != null)
        {
            _humanIteractBrickComponent.TryTakeBrick(other.gameObject);
            RemoveTarget(brickGroundState.transform);
        }
    }
}
