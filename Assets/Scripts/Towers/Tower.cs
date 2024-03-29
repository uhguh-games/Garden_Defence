using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool towerActive;
    private TowerSpawner towerSpawner;
    [SerializeField] LayerMask blockedLayers;

    [Header("Enemy Detection")]
    Collider[] colliders;
    [SerializeField] public float range = 3.5f;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public List<Monster> enemiesInRange;
    [SerializeField] public Monster targetedEnemy;
    [SerializeField] public float scanningDelay = 0.1f;
    public float scanningTimer;

    void Awake() 
    {
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
    }

    public void ActivateTower() 
    {
        towerActive = true;
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (blockedLayers == (blockedLayers | (1 << collision.gameObject.layer))) 
        {
            // print ("Tower collided with " + collision.gameObject.name);
            towerSpawner.spaceBlocked = true;
        }
    }

    void OnCollisionExit(Collision collision) 
    {
        towerSpawner.spaceBlocked = false;
    }

    public void ScanForEnemies()
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

    public void ResetEnemyList() // when an object is disabled like the firepit
    {
        enemiesInRange.Clear();
        targetedEnemy = null;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
