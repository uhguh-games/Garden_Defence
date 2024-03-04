using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Script where all object pools are started and referenced from
    private ObjectPool cannonBallPool;
    private ObjectPool junkPool;
    [SerializeField] CannonBall cannonBallPrefab;
    [SerializeField] ResourceJunk junkPrefab;
    
    void Awake() 
    {
        cannonBallPool = ObjectPool.CreateInstance(cannonBallPrefab, 50);
        junkPool = ObjectPool.CreateInstance(junkPrefab, 50);
    }
}
