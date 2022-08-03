using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour,IWinable
{
    private Transform _target;
    public virtual void Win(Transform target, int top)
    {
        FinishLevelUIManager.Singleton.OnEnd(top);
        _target = target;
        StartCoroutine(ToTarget(target));
        GetComponent<StateController>().State = (int) PlayerState.Win;
    }

    protected IEnumerator ToTarget(Transform target)
    {
        for (int i = 0; i < 1000; i++)
        {
            transform.position=Vector3.Lerp(transform.position,target.position,0.1f);
            transform.rotation=Quaternion.Lerp(transform.rotation,target.rotation,0.1f);
            if ((transform.position - target.position).magnitude < 0.1f)
            {
                break;
            }
            yield return null;
        }
    }
}
