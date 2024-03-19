using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    private Transform deathPos;
    public float maxHealth = 10f;
    public float currentHealth;
    private HealthBar healthBar;
    [SerializeField] Transform hitTarget; // empty object on the enemy
    private PoolManager poolManager;
    private void Awake()
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
    }
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
            deathPos = this.transform;

            // print ("Killed " + this.gameObject.name);

            DropJunk();

            this.gameObject.SetActive(false); // Enemy gets returned into its' pool
        }
    }

    public void TakeDamage(float damage) 
    {
        float percentage = damage / maxHealth;

        float actualDamage = maxHealth * percentage;
        
        currentHealth -= actualDamage;
        healthBar.SetHealth(currentHealth);
    }

    public Transform getHitTarget() 
    {
        return hitTarget;
    }

    private void DropJunk()
    {
        PoolableObject instance = poolManager.junkPool.GetObject();
        ResourceJunk junkPrefab = instance as ResourceJunk;
        instance.transform.position = deathPos.position;
    }

    void OnTriggerEnter(Collider other) // temporary cringy solution
    {
        if (other.tag == "Temp")
        {
            this.gameObject.SetActive(false); // Enemy gets returned into its' pool
        }
    }
}
