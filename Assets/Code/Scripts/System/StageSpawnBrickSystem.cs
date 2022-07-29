using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class StageSpawnBrickSystem : MonoBehaviour
{

    [SerializeField] private Transform[] Stairs;
    
    
    public GameObject Brick;
    public MeshFilter MeshFilter;
    [Range(0,3)]
    public int SubDivideTimes;
    private Dictionary<int, byte> freeVerPoint;
    private int vertexCount;



    public Dictionary<GameObject, ObjectPooling> pollBricks;
    private void Awake()
    {
        freeVerPoint = new Dictionary<int, byte>();
        pollBricks = new Dictionary<GameObject, ObjectPooling>();
    }

    public Vector3 GetRandomPosisionOnMesh()
    {
        return transform.TransformPoint(MeshFilter.sharedMesh.vertices[UnityEngine.Random.Range(0, vertexCount)]);
    }

    public Vector3 GetRandomStair()
    {
        return Stairs[UnityEngine.Random.Range(0, Stairs.Length)].position;
    }

    private void Start()
    {
        var mesh = MeshFilter.sharedMesh;
        

        MeshFilter.sharedMesh = Instantiate(mesh);
        
        // subdivision mesh
        for (int i = 0; i < SubDivideTimes; i++)
        {
            MeshHelper.Subdivide(MeshFilter.sharedMesh);
        }

        // vertex count
        var verticlesCount = MeshFilter.sharedMesh.vertexCount;
        vertexCount = verticlesCount;
        // initial freeVerPoint
        for (int i = 0; i < verticlesCount; i++)
        {
            freeVerPoint.Add(i,0);
        }

        
        // player + enemy
        var totalHuman = LevelSystem.Singleton.Enemy.Length;
        var countPerHuman =(int)(verticlesCount / totalHuman);
        
        
        for (int i = 0; i < LevelSystem.Singleton.Enemy.Length; i++)
        {
            var enemyGO = LevelSystem.Singleton.Enemy[i];
            var skinComponent = enemyGO.GetComponent<HumanSkinComponent>();
            var brickPooling = new ObjectPooling(enemyGO, Brick, verticlesCount);
            
            pollBricks.Add(enemyGO,brickPooling);
            
    

            List<GameObject> diasableAfterDone = new List<GameObject>();
            // setMaterial inherit from the owner
            for (int j = 0; j < verticlesCount; j++)
            {
                var instantiate= brickPooling.Instantiate();

                var brickStatComponent = instantiate.GetComponent<BrickController>();

                
                var material = new Material(Brick.GetComponent<MeshRenderer>().sharedMaterial);
                material.SetColor("_MainColor",skinComponent.Material.GetColor("_MainColor"));

                brickStatComponent.Initial(this,enemyGO,material);
                diasableAfterDone.Add(instantiate);
            }

            foreach (var go in diasableAfterDone)
            {
                go.SetActive((false));
            }
        }
        FirstSpawn();
    }

    private void OnEnable()
    {
    }

    /*private void Update()
    {
        Debug.Log(freeVerPoint.Count);
    }*/

    private void FirstSpawn(){
        var totalHuman = LevelSystem.Singleton.Enemy.Length;
        var verticlesCount = MeshFilter.mesh.vertexCount;
        var countPerHuman =(int)(verticlesCount / totalHuman);


        for (int i = 0; i < totalHuman; i++)
        {
            var go = LevelSystem.Singleton.Enemy[i];
            var simplePoll = pollBricks[go];
            
         
                for (int  j= 0; j < countPerHuman; j++)
                {
                    if (TryUseRandomPointInMesh(out var v))
                    {
                        var newInstance = simplePoll.Instantiate();
                        var brickStat= newInstance.GetComponent<BrickController>();
                        brickStat.keyIndexInMesh = v.Key;
                    }
                }
            
            
        }
    }


    public void Recycle(int index)
    {
        if (!freeVerPoint.ContainsKey(index))
        {
            freeVerPoint.Add(index,0);
        }
    }

    public void ReSpawn(GameObject go)
    {
        var simplePoll = pollBricks[go];
        if (simplePoll!=null)
        {
            for (int i = 0; i < 7; i++)
                {
                    if (TryUseRandomPointInMesh(out var v))
                    {
                        var newInstance = simplePoll.Instantiate(GetPosisionByKeyIndex(v.Key),Quaternion.identity);
                        var brickStat= newInstance.GetComponent<BrickController>();
                        brickStat.keyIndexInMesh = v.Key;
                    }
                }
            
        }
    }

    private void UseKey(int key)
    {
        freeVerPoint.Remove(key);
    }
        
    public KeyValuePair<int,byte> GetRandomPointInMesh()
    {
        var randomValuePair = freeVerPoint.ElementAt(UnityEngine.Random.Range(0, freeVerPoint.Count));
        return randomValuePair;
    }

    public bool TryUseRandomPointInMesh(out KeyValuePair<int, byte> value)
    {
        if (freeVerPoint.Count <= 0)
        {
            value = default;
            return false;
        }
        
        var randomValuePair = freeVerPoint.ElementAt(UnityEngine.Random.Range(0, freeVerPoint.Count));
        freeVerPoint.Remove(randomValuePair.Key);
        value = randomValuePair;
        return true;
    }

    public Vector3 GetPosisionByKeyIndex(int index)
    {
        var wp = transform.TransformPoint(MeshFilter.sharedMesh.vertices[index]);
        return wp;
    }

    public bool TryGetRandomPosisionOnMesh(out Vector3 posision)
    {
        if (TryUseRandomPointInMesh(out var p))
        {
            posision = GetPosisionByKeyIndex(p.Key);
            return true;
        }
        posision=Vector3.zero;
        return false;
    }
}
