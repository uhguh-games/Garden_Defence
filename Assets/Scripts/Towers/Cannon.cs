using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Enemy Detection")]
    [SerializeField] float range = 3.5f;
    [SerializeField] LayerMask enemyLayer;
    Collider[] colliders;
    [SerializeField] List<Monster> enemiesInRange;
    [SerializeField] Monster targetedEnemy;
    [SerializeField] float scanningDelay = 0.1f;
    float scanningTimer;
    float fireTimer;
    private Tower tower;
    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] Transform firePoint;
    private PoolManager poolManager;


    private void Awake() 
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        tower = GetComponent<Tower>();
    }

    private void Update() 
    {
        if (tower.towerActive)
        {
            scanningTimer += Time.deltaTime;

            if (scanningTimer >= scanningDelay)
            {
                scanningTimer = 0;
                ScanForEnemies();
            }
            
            if (targetedEnemy)
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

    private void ScanForEnemies() 
    {
        colliders = Physics.OverlapSphere(transform.position, range, enemyLayer);

        enemiesInRange.Clear();

        foreach(Collider collider in colliders) 
        {
            enemiesInRange.Add(collider.GetComponent<Monster>());
        }

        if (enemiesInRange.Count != 0) 
        {
            targetedEnemy = enemiesInRange[0];
        }
    }
    
    private void Fire()
    {
        if (targetedEnemy != null) 
        {
            Vector3 enemyDirection = targetedEnemy.transform.position - firePoint.position.normalized;
        
            PoolableObject pooledObject = poolManager.cannonBallPool.GetObject();
            // PoolableObject pooledObject = cannonBallPool.GetObject();
            CannonBall cannonBall = pooledObject as CannonBall;

            if (cannonBall != null) 
            {
                cannonBall.Setup(enemyDirection, targetedEnemy);
                cannonBall.transform.SetParent(transform, false);
                cannonBall.transform.position = firePoint.transform.position;
            }
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
