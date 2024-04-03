using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{
    /* 
    When enabled (plopped on the ground):
    - Light up (enable particle effect) - OK
    
    IDEA: Since all the towers are using the enemy detection, put it in a separate script and have that information shared among all the towers - OK

    - Create an enemy detection radius -> Access through the Tower script OK
    - In FirePit make it possible to disable the enemy detection radius if fire pit is out OK (fireActive)

    - Create a (visual) light radius that matches the enemy detection radius
    - Start counter OK
    - When counter ends - fire goes out (disable light and enemy detection radius's) OK
    - Lights out: (Disable particle effect) OK

    - In StoneSlinger: if it's night -> find the closest firepits enemy detection radius if enemy is in that radius and the towers own shooting range radius -> shoot ELSE -> display message "too dark to detect enemies"
    To test fire setup/reset:
    if The F key is pressed on the keyboard and th fire is not already active - ignite the flame OK
    - Current implementation will ignite ALL firepits in the scene, this will be fixed when the drag and drop scrap placement controls are made. The drag and dropped item will find which fire pit it was dropped into.

    When user clicks on the fire/or maybe drags a piece of junk from their inventory to the fire:
    - "Ignite"

    In a separate script create the controls for the player to drag and drop a piece of scrap from their resource panel into a firepit object which will call the SetupFire() method from this script
    */
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

        if (Input.GetKeyDown(KeyCode.F) && !fireActive) 
        {
            StartCoroutine(SetupFireRoutine());
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
