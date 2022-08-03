using System.Collections;
using System.Collections.Generic;
using Redcode.Paths;
using UnityEngine;

public class HumanIteractWithBrickComponent : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private StackController _stackController;
    

    public void TryTakeBrick(GameObject brick)
    {
        if(brick==null) return;
        var brickStat = brick.GetComponent<BrickStatComponent>();
        if (brickStat != null)
        {
            if (brickStat.Owner == gameObject||brickStat.Owner==null)
            {
                brickStat.Owner = gameObject;
                //take brick
                var brickGroundState = brick.GetComponent<BrickGroundState>();
                if (brickGroundState != null)
                {
                    
                    
                    var direcToBrick = brick.transform.position - transform.position;
                    direcToBrick.y = 0;
                    direcToBrick.Normalize();
                    direcToBrick *= (0.3f+_stackController.StackHeight/3);
                    
                    brickGroundState.OnBeTake(_stackController.transform,_stackController.GetNextLPosision(),direcToBrick);
                    _stackController.AddStack(brick);
                }
            }
        }
    }
}
