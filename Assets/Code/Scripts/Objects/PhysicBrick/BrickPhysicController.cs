using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPhysicController : MonoBehaviour
{

    [SerializeField] private LayerMask _groundMask;
    
    public void OnAdd(Transform brick)
    {
        transform.position = brick.position;
        transform.rotation = brick.rotation;
        brick.SetParent(transform);
    }
    
    public void OnRemove()
    {
        if (transform.childCount > 0)
        {
            var brick = transform.GetChild(0);
            brick.SetParent(null);
        }
        
        gameObject.SetActive(false);
    }


    public void OnAddPhysic(Transform brick)
    {
        transform.position = brick.position;
        transform.rotation = brick.rotation;
        brick.SetParent(transform);
        GetComponent<Rigidbody>().velocity = UnityEngine.Random.insideUnitSphere*3f;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).SetParent(null);
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
