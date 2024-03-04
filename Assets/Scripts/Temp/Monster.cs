using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*
    This is a script used for testing - it will be deleted later
    */
    [SerializeField] EventManagerSO eventManager;
    [SerializeField] private ResourceJunk junkPrefab;
    private ObjectPool junkPool;
    private Transform deathPos;

    public float maxHealth = 10f;
    public float currentHealth;
    private HealthBar healthBar;
    [SerializeField] Transform hitTarget; // empty object on the enemy

    private void Awake()
    {
        junkPool = ObjectPool.CreateInstance(junkPrefab, 50);

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
            print ("Killed " + this.gameObject.name);
            DropJunk();
            Destroy(this.gameObject);
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
        PoolableObject instance = junkPool.GetObject();
        ResourceJunk junkPrefab = instance as ResourceJunk;
        instance.transform.position = deathPos.position;
    }
}
