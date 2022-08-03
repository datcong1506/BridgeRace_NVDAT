using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


[RequireComponent(typeof(EnemyMovementComponent))]
[RequireComponent(typeof(EnemyIteractWithBrickComponent))]
public class EnemyMovementState : AbStatePatternState
{
    private HumanMovementComponent _humanMovementComponent;
    private HumanIteractWithBrickComponent _humanIteractBrickComponent;
    private HumanIterractWithStairComponent _humanIterractWithStairComponent;

    [SerializeField] private StackController _stackController;

    
    //AI Movement
    public GameObject CurrentStage;
    public StairController CurrentStair;
    public List<Transform> CurrentBricks;



    [Header("Collect Brick SubState")] [SerializeField]
    private int MinNumBrick;
    [SerializeField] private int MaxNumBrick;
    private int TargetNumBrick;
    
    public void AddTarget(Transform newBrick)
    {
        for (int i = 0; i < CurrentBricks.Count; i++)
        {
            if (CurrentBricks[i] == newBrick)
            {
                return;
            }
        }  
        CurrentBricks.Add(newBrick);
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
    
    //


    private void GoToTakeBrick()
    {
        if (CurrentBricks != null)
        {
            if (CurrentBricks.Count > 0)
            {
                switch (CurrentBricks[0].GetComponent<AbStatePatternIFSM>().currentState)
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
                        }
                        else
                        {
                            _humanMovementComponent.MoveToTarget(CurrentBricks[0].transform.position);
                        }
                        break;
                }
            }
        }
        
    }

    private void GoUpStair()
    {
        
    }

    private void AttackHuman()
    {
        
    }
    
    
    
    private void Start()
    {
        _humanMovementComponent = GetComponent<HumanMovementComponent>();
        _humanIteractBrickComponent = GetComponent<HumanIteractWithBrickComponent>();
        _humanIterractWithStairComponent = GetComponent<HumanIterractWithStairComponent>();

        CurrentBricks = new List<Transform>();
    }

    public override ResponseEvent TriggerEvent(GameEvent gameEvent)
    {
        return null;
    }

    public override void OnEnterState()
    {
        /*var testTarget = new Vector3(4.19000006f,20.2049999f,56.4469986f);
        _humanMovementComponent.MoveToTarget(testTarget);*/
        
    }

    public override void OnUpdateState()
    {
        GoToTakeBrick();
    }

    public override void OnExitState()
    {
    }

    public override bool DescistionToThisState()
    {
        return true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (abStatePatternIFSM.currentState != this)
        {
            return;
        }

        // iterract with brick handle
        var brickGroundState = other.GetComponent<BrickGroundState>();
        if (brickGroundState != null)
        {
            _humanIteractBrickComponent.TryTakeBrick(other.gameObject);
            RemoveTarget(brickGroundState.transform);
            return;
        }
        // iteract with stair
        var stairStepController = other.GetComponent<StairStepController>();
        if (stairStepController != null)
        {
            _humanIterractWithStairComponent.OnHitStairStep(other.gameObject);
        }
        
        
        //enemy__

        
    }
}
