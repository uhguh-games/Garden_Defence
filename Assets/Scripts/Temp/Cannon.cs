using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Enemy Detection")]
    [SerializeField] float range = 3.5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Monster> enemiesInRange;
    [SerializeField] Monster targetedEnemy;
    float scanningTimer;
    float scanningDelay = 0.1f;


    private bool cannonActive = true;
    float fireTimer;
    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] CannonBall cannonBallPrefab;
    [SerializeField] Transform firePoint;
    // [SerializeField] private int fireRate = 10;
    private ObjectPool cannonBallPool;

    private void Awake() 
    {
        cannonBallPool = ObjectPool.CreateInstance(cannonBallPrefab, 50);
    }

    private void Update() 
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // StartCoroutine(Shoot());
            Fire();
        }
        */

        if (cannonActive)
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
            CannonBall cannonBall = pooledObject as CannonBall; // Attempt to cast the pooled object to CannonBall

            if (cannonBall != null) 
            {
                cannonBall.Setup(enemyDirection, targetedEnemy); // Call Setup() on the CannonBall
                cannonBall.transform.SetParent(transform, false);
                cannonBall.transform.position = firePoint.transform.position;
            }
        }

        /*
        PoolableObject instance = cannonBallPool.GetObject();
        instance.transform.SetParent(transform, false);
        instance.transform.position = firePoint.transform.position;
        */
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void ActivateCannon() 
    {
        cannonActive = true;
    }

    #region IEnumerator
    /*
    private IEnumerator Shoot() 
    {
        #region One at the time
        PoolableObject instance = cannonBallPool.GetObject();
        instance.transform.SetParent(transform, false);
        instance.transform.position = firePoint.transform.position;

        yield return new WaitForSeconds(5f);

        // WaitForSeconds wait = new WaitForSeconds(1f / fireRate);
        #endregion

        #region Rapid fire

        while(true)
        {
            PoolableObject instance = cannonBallPool.GetObject();

            if (instance != null) 
            {
                instance.transform.SetParent(transform, false);
                instance.transform.position = firePoint.transform.position;
                // instance.transform.localPosition = Vector3.zero;
            }

            yield return wait;
        }

        #endregion
    }
    */
    #endregion

    #region Outdated Cannon
    /*
    private void ShootCannonBall() 
    {
        Vector3 cannonBallSpawnPoint = firePoint.transform.position;
        GameObject newCannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint, Quaternion.identity);
        newCannonBall.transform.parent = firePoint.transform;
    }
    */
    #endregion
}
