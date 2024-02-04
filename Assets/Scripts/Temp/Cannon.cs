using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    /*
    This is a script used for testing - it will be deleted later
    */

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] GameObject firePoint;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ShootCannonBall();
        }
    }

    private void ShootCannonBall() 
    {
        Vector3 cannonBallSpawnPoint = firePoint.transform.position;
        GameObject newCannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint, Quaternion.identity);
        newCannonBall.transform.parent = firePoint.transform;
    }
}
