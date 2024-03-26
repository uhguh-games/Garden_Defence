using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{
    /* When enabled (plopped on the ground):
    - Light up (enable particle effect)
    - Create an enemy detection radius
    - Create a light radius that matches the enemy detection radius
    - Start counter
    - When counter ends - fire goes out (disable light and enemy detection radius's)
    - Lights out: (Disable particle effect)

    When user clicks on the fire/or maybe drags a piece of junk from their inventory to the fire:
    - "Ignite"
    */
    [SerializeField] GameObject fireEffect;
    private Tower tower;

    void Awake() 
    {
        tower = GetComponent<Tower>();
        fireEffect.SetActive(false);
    }

    void Update() 
    {
        if (tower.towerActive) 
        {
            SetupFire();
        }

    }

    void SetupFire() 
    {
        fireEffect.SetActive(true);
        // Create a light radius
        // Create an enemy detection radius

    }


}
