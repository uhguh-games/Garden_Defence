using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public ObjectPool stonePool; // <- accessing directly works but I should try make the fancy way work.
    public ObjectPool junkPool;
    [SerializeField] StoneProjectile stonePrefab;
    [SerializeField] ResourceJunk junkPrefab;
    
    void Awake() // pools are created here
    {
        stonePool = ObjectPool.CreateInstance(stonePrefab, 50);
        junkPool = ObjectPool.CreateInstance(junkPrefab, 50);
    }

    // Method that returns each pool as public so other scripts have access to them safely:
    public ObjectPool GivePool(ObjectPool objectPool) 
    {
        return objectPool;
    }
}
