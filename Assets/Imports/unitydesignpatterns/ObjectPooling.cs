using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPooling
{
    private List<GameObject> poolling;

    private GameObject ownerObject;
    private GameObject poolObject;
    private GameObject container;

    public ObjectPooling(GameObject owner, GameObject pool, int initialNumber = 20)
    {
        if (owner == null || pool == null || initialNumber < 1) return;
        ownerObject = owner;
        poolObject = pool;
        container = new GameObject(owner.name + "_pool_" + poolObject.name + "_container");


        poolling = new List<GameObject>();
        for (int i = 0; i < initialNumber; i++)
        {
            var newInstance = GameObject.Instantiate(poolObject, Vector3.zero, Quaternion.identity);
            newInstance.transform.parent = container.transform;
            newInstance.SetActive(false);
            poolling.Add(newInstance);
        }
    }

    public ObjectPooling(GameObject owner, GameObject pool, Transform parent,int initialNumber = 20)
    {
        if (owner == null || pool == null || initialNumber < 1) return;
        ownerObject = owner;
        container = parent.gameObject;
        poolObject = pool;


        poolling = new List<GameObject>();
        for (int i = 0; i < initialNumber; i++)
        {
            var newInstance = GameObject.Instantiate(poolObject, pool.transform.position, pool.transform.rotation);
            newInstance.transform.parent = container.transform;
            newInstance.transform.localScale = pool.transform.localScale;
            newInstance.SetActive(false);
            poolling.Add(newInstance);
        }
    }

    public GameObject Instantiate(Vector3 posision, Quaternion rotation)
    {
        for (int i = 0; i < poolling.Count; i++)
        {
            if (!poolling[i].activeInHierarchy)
            {
                poolling[i].SetActive(true);
                poolling[i].transform.position = posision;
                poolling[i].transform.rotation = rotation;
                
                return  poolling[i];
            }
        }
        var newInstance = GameObject.Instantiate(poolObject, posision, rotation);
        newInstance.transform.parent = container.transform;
        poolling.Add(newInstance);
        return newInstance;
    }

    public GameObject Instantiate()
    {
        for (int i = 0; i < poolling.Count; i++)
        {
            if (!poolling[i].activeInHierarchy)
            {
                poolling[i].SetActive(true);
                return poolling[i];
            }
        }
        var newInstance = GameObject.Instantiate(poolObject, poolObject.transform.position, poolObject.transform.rotation);
        newInstance.transform.parent = container.transform;
        newInstance.transform.localScale = poolObject.transform.localScale;
        poolling.Add(newInstance);
        return newInstance;
    }

    public void DestroyPool()
    {
        if (container == null)
        {
            for (int i = 0; i < poolling.Count; i++)
            {
                if (poolling[i] != null)
                {
                    GameObject.Destroy(poolling[i]);
                }
            }
            return;
        }
        if (container.name == ownerObject.name + "_pool_" + poolObject.name + "_container")
        {
            GameObject.Destroy(container);
        }
        else
        {
            for (int i = 0; i < poolling.Count; i++)
            {
                GameObject.Destroy(poolling[i]);
            }
        }
    }




}
