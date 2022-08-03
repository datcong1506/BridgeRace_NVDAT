using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Paths;
using UnityEngine;
using UnityEngine.Events;

public class StackController : MonoBehaviour
{
    [SerializeField]private List<GameObject> stacks;
    [SerializeField] private float _offsetBetweenStacks;
    [SerializeField] private UnityEvent onAddStackEvent;
    
    public float StackHeight
    {
        get
        {
            return  stacks.Count*_offsetBetweenStacks;
        }
    }

    public int StackCount
    {
        get
        {
            return stacks.Count;
        }
    }
    
    public Transform Transform;
    
    
    private void Start()
    {
        stacks=new List<GameObject>();
        Transform = transform;
    }
    
    public void AddStack(GameObject newStack)
    {
        stacks.Add(newStack);
        onAddStackEvent?.Invoke();
    }

    

    public void Drop()
    {
        for (int i = 0; i < StackCount; i++)
        {
            var st = stacks[i];
            var ctl = st.GetComponent<BrickController>();
            ctl._stageSpawnBrickSystem.ReSpawn(ctl._owner);
            break;
        }
        for (int i = 0; i < StackCount; i++)
        {
            var st = stacks[i];
            var ctl = st.GetComponent<BrickController>();
            ctl.OnDrop();
        }

        
        stacks.Clear();
    }
    
    public void RemoveStack()
    {
        if (stacks.Count == 1)
        {
            
        }
        if (stacks.Count > 0)
        {
            var stack = stacks[stacks.Count - 1];
            var recycle = stack.GetComponent<BrickController>();
            recycle.Recycle();
            stacks.RemoveAt(stacks.Count-1);
        }
    }

    private void GetTargetPosisionByStackIndex()
    {
    }

    public Vector3 GetNextWPosision()
    {
        return transform.TransformPoint(new Vector3(0, _offsetBetweenStacks, 0) * stacks.Count);
    }

    public Vector3 GetNextLPosision()
    {
        return new Vector3(0, _offsetBetweenStacks, 0) * stacks.Count;
    }

    public GameObject GetStack(int index)
    {
        if (index >= 0 && index < StackCount)
        {
            return stacks[index];
        }

        return null;
    }
    
}
