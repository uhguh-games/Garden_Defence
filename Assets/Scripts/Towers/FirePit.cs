using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{
    [SerializeField] GameObject fireEffect;
    [SerializeField] bool fireActive = true;
    [SerializeField] float fireActiveTimer;

    [Tooltip("How long does the fire last")]
    [SerializeField] float fireActiveTimerValue;
    private Tower tower;

    void Awake() 
    {
        tower = GetComponent<Tower>();
        fireEffect.SetActive(false);
        fireActiveTimer = fireActiveTimerValue;
    }

    void Update() 
    {
        if (tower.towerActive && fireActive) 
        {
            SetupFire(); // handles visuals and audio
            fireActiveTimer -= Time.deltaTime;

            tower.scanningTimer += Time.deltaTime;

            if (tower.scanningTimer >= tower.scanningDelay)
            {
                tower.scanningTimer = 0;
                tower.ScanForEnemies();
            }

            if (fireActiveTimer <= 0)
            {
                DisableFire();

                fireActive = false;
                fireActiveTimer = 0f;
                tower.ResetEnemyList();
            }
        }
    }
    public void ReActivateFire() 
    {
        StartCoroutine(SetupFireRoutine());
    }

    public void ResetFireActiveTimer() 
    {
        if (!fireActive) 
        {
            fireActiveTimer = fireActiveTimerValue;
        }
    }
    
    void SetupFire() // visuals and audio go here
    {
        fireEffect.SetActive(true);
        // visual light radius has to match with the enemy detection radius (assigned in through the tower script)
    }

    void DisableFire() // visuals and audio go here
    {
        fireEffect.SetActive(false);
    }

    IEnumerator SetupFireRoutine() 
    {
        ResetFireActiveTimer();
        fireActive = true;

        SetupFire();

        while (fireActiveTimer > 0)
        {
            fireActiveTimer -= Time.deltaTime;
            yield return null;
        }

        fireActive = false;
        DisableFire();
    }
}
