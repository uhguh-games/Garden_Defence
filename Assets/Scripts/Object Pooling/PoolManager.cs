using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public ObjectPool stonePool;
    public ObjectPool junkPool;
    [SerializeField] StoneProjectile stonePrefab;
    [SerializeField] ResourceJunk junkPrefab;
    
    void Awake() // pools are created here
    {
        stonePool = ObjectPool.CreateInstance(stonePrefab, 50);
        junkPool = ObjectPool.CreateInstance(junkPrefab, 50);
    }

    public ObjectPool GivePool(ObjectPool objectPool) 
    {
        return objectPool;
    }
}
