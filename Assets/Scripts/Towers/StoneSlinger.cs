using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSlinger : MonoBehaviour
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
    [SerializeField] GameObject towerTop;
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
            Vector3 enemyDirection = targetedEnemy.transform.position - firePoint.position;
            enemyDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(enemyDirection, Vector3.up); 
            Quaternion adjustedRotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, 0); 

            towerTop.transform.rotation = adjustedRotation;

            PoolableObject pooledObject = poolManager.stonePool.GetObject();
            StoneProjectile stone = pooledObject as StoneProjectile;

            if (stone != null) 
            {
                stone.Setup(enemyDirection, targetedEnemy);
                stone.transform.SetParent(transform, false);
                stone.transform.position = firePoint.transform.position;
            }
        }
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
