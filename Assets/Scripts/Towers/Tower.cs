using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool towerActive;
    private TowerSpawner towerSpawner;
    [SerializeField] LayerMask blockedLayers;

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
}
