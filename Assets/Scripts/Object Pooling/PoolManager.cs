using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Script where all object pools are started and referenced from

    // private ObjectPool cannonBallPool;
    // private ObjectPool junkPool;
    public ObjectPool cannonBallPool; // <- accessing directly works but I should try make the fancy way work.
    public ObjectPool junkPool;
    [SerializeField] CannonBall cannonBallPrefab;
    [SerializeField] ResourceJunk junkPrefab;
    
    void Awake() // pools are created here
    {
        cannonBallPool = ObjectPool.CreateInstance(cannonBallPrefab, 50);
        junkPool = ObjectPool.CreateInstance(junkPrefab, 50);
    }

    // Method that returns each pool as public so other scripts have access to them safely:
    public ObjectPool GivePool(ObjectPool objectPool) 
    {
        return objectPool;
    }
}
