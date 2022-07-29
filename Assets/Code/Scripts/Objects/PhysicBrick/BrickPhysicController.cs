using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickPhysicController : MonoBehaviour
{

    [SerializeField] private LayerMask _groundMask;
    
    public void OnAdd(Transform brick)
    {
        transform.position = brick.position;
        transform.rotation = brick.rotation;
        brick.SetParent(transform);
        
        
        // add force 
        brick.localPosition=Vector3.zero;
        brick.localRotation=Quaternion.identity;
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

    private void OnDisable()
    {
        transform.position=Vector3.up*1000f;
    }

    public void OnAddPhysic(Transform brick)
    {
        GetComponent<Rigidbody>().velocity = UnityEngine.Random.insideUnitSphere*3f;

        OnAdd(brick);
        
        /*transform.position = brick.position;
        transform.rotation = brick.rotation;
        brick.SetParent(transform);
        
        
        // add force 
        brick.localPosition=Vector3.zero;
        brick.localRotation=Quaternion.identity;*/
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(DelayOneFarme(() =>
                {
                    transform.GetChild(i).localPosition = Vector3.zero;
                    transform.GetChild(i).GetComponent<SphereCollider>().enabled = true;
                    transform.GetChild(i).GetComponent<BrickController>().OnGround();
                }));
                
                break;
            }
        }
    }

    IEnumerator DelayOneFarme(Action f)
    {
        yield return  new WaitForFixedUpdate();
        f();
    }
}
