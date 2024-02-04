using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*
    This is a script used for testing - it will be deleted later
    */

    public float maxHealth = 10f;
    public float currentHealth;
    private HealthBar healthBar;

    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() 
    {
        if (currentHealth <= 0) 
        {
            print ("Killed " + this.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {
        float percentage = damage / maxHealth;

        print("Percent damage taken: " + Mathf.Round(percentage * 100f) / 100f);
        print (percentage);

        float actualDamage = maxHealth * percentage;
        
        print("Actual damage: " + Mathf.Round(actualDamage * 100f) / 100f);
        print (actualDamage);

        currentHealth -= actualDamage;
        healthBar.SetHealth(currentHealth);
    }
}
