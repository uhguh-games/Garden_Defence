using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSlinger : MonoBehaviour
{
    [Header("Shooting")]
    
    float fireTimer;
    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject towerTop;
    Tower tower;
    PoolManager poolManager;


    private void Awake() 
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        tower = GetComponent<Tower>();
    }

    private void Update() 
    {
        if (tower.towerActive)
        {
            tower.scanningTimer += Time.deltaTime;

            if (tower.scanningTimer >= tower.scanningDelay)
            {
                tower.scanningTimer = 0;
                tower.ScanForEnemies();
            }
            
            if (tower.targetedEnemy)
            {
                fireTimer += Time.deltaTime;
            }

            if (fireTimer >= fireDelay)
            {
                fireTimer = 0f;
                Fire();
            }
        }
    }

   
    
    private void Fire()
    {
       if (tower.targetedEnemy != null) 
       {
            Vector3 enemyDirection = tower.targetedEnemy.transform.position - firePoint.position;
            enemyDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(enemyDirection, Vector3.up); 
            Quaternion adjustedRotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, 0); 

            towerTop.transform.rotation = adjustedRotation;

            PoolableObject pooledObject = poolManager.stonePool.GetObject();
            StoneProjectile stone = pooledObject as StoneProjectile;

            if (stone != null) 
            {
                stone.Setup(enemyDirection, tower.targetedEnemy);
                stone.transform.SetParent(transform, false);
                stone.transform.position = firePoint.transform.position;
            }
        }
    }
}
