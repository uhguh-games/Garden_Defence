using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] CannonBall cannonBallPrefab;
    [SerializeField] GameObject firePoint;
    private int fireRate = 10;
    private ObjectPool cannonBallPool;

    private void Awake() 
    {
        cannonBallPool = ObjectPool.CreateInstance(cannonBallPrefab, 50);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() 
    {
        WaitForSeconds wait = new WaitForSeconds(1f / fireRate);

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
    }

    #region Outdated Cannon Instantiation
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
