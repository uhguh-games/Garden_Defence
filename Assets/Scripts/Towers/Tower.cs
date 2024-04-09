using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public List<Monster> litEnemies = new List<Monster>();
    [SerializeField] public Monster targetedEnemy;
    [SerializeField] public float scanningDelay = 0.1f;
    public float scanningTimer;
    private HexGrid hexGrid;

    void Awake() 
    {
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
    }

    public void ActivateTower() 
    {
        towerActive = true;
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (blockedLayers == (blockedLayers | (1 << collision.gameObject.layer))) 
        {
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

        ClearDestroyedEnemies();
    }

    public void GetLitEnemies()
    {
        litEnemies.Clear();

        foreach (var kvp in hexGrid.ItemsInScene)
        {
            if (kvp.Key.name == "FirePit(Clone)") // in the future other light towers can be added here
            {
                GameObject firePit = kvp.Key;

                Tower towerScript = firePit.GetComponent<Tower>();

                if (towerScript != null) 
                {
                    for (int i = 0; i < towerScript.enemiesInRange.Count; i++) 
                    {
                        litEnemies.Add(towerScript.enemiesInRange[i]);
                    }
                }
            }
        }

        CompareEnemyList();
    }

    public void CompareEnemyList() // compares towers enemy list to all light source objects enemy lists
    {
        int minLength = Mathf.Min(enemiesInRange.Count, litEnemies.Count);
    
        for (int i = 0; i < minLength; i++) 
        {
            if (enemiesInRange[i] == litEnemies[i])
            {
                print("Match found");
                // get the matching enemy 
                // set it to targeted enemy

                // flag true <- what flag?
          
            }
        }
    }

    public void ClearDestroyedEnemies() // removes dead enemies from the list
    {
        List<Monster> remainingEnemies = enemiesInRange.Where(enemy => enemy != null && enemy.gameObject.activeSelf).ToList();
        enemiesInRange.Clear();
        enemiesInRange.AddRange(remainingEnemies);
    }

    public void ResetEnemyList() // when an object is disabled like the firepit
    {
        enemiesInRange.Clear();
        targetedEnemy = null;
    }

    private void OnDrawGizmosSelected() // visualises towers radius in the scene
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
