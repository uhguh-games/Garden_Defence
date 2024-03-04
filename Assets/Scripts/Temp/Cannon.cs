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
    float scanningTimer;
    float scanningDelay = 0.1f;
    float fireTimer;
    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] CannonBall cannonBallPrefab;
    [SerializeField] Transform firePoint;
    private ObjectPool cannonBallPool;
    private Tower tower;

    private void Awake() 
    {
        cannonBallPool = ObjectPool.CreateInstance(cannonBallPrefab, 50); // Move this to a different script
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
        
            PoolableObject pooledObject = cannonBallPool.GetObject();
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
