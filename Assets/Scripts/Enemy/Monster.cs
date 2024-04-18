using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    private Transform deathPos;
    public float maxHealth = 10f;
    public int eatingTime;
    public float currentHealth;
    private HealthBar healthBar;
    [SerializeField] Transform hitTarget; // empty object on the enemy
    private PoolManager poolManager;
    private Crop cropToEat;
    private int enemyJunkValue; // set by the economy manager
    EconomyManager economyManager;

    [Tooltip("Drag in enemys scriptable object")]
    [SerializeField] EnemyScriptableObject enemyStats;
    private EnemyType enemyType;

    private void Awake()
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
    }
    void Start()
    {
        enemyType = enemyStats.Type;
        healthBar = GetComponentInChildren<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateEnemyJunkValue();
    }

    void Update() 
    {
        if (currentHealth <= 0) 
        {
            deathPos = this.transform;
            eventManager.OnKill();
            DropJunk();
            this.gameObject.SetActive(false); // Enemy gets returned into the pool
        }
    }

    void OnEnable() 
    {
        economyManager.OnEnemyJunkValueChange += UpdateEnemyJunkValue;
    }

    void OnDisable() 
    {
        economyManager.OnEnemyJunkValueChange -= UpdateEnemyJunkValue;
    }

    private void UpdateEnemyJunkValue() 
    {
        string enemyTypeString = enemyType.ToString();
        enemyJunkValue = economyManager.GetEnemyJunkValue(enemyTypeString);
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
        instance.GetComponent<ResourceJunk>().SetJunkValue(enemyJunkValue);
        instance.transform.position = deathPos.position;
    }

    void OnTriggerEnter(Collider other) // temporary cringy solution 
    {
        if (other.tag == "Temp") 
        {
            this.gameObject.SetActive(false); // Enemy gets returned into its' pool
        }
        if (other.tag == "CropEaten") //if the enemy finds a crop whose tag was changed to CropEaten when they chose the crop to eat
        {
            cropToEat = other.GetComponent<Crop>();
           // Debug.Log("Collided with crop");
            StartCoroutine(eating());
        }
    }

    IEnumerator eating() //eat for n seconds
    {
        yield return new WaitForSeconds(eatingTime);
       // Debug.Log("Eating finished");
        cropToEat.GetEaten(); //tell crop to delete itself and to add the points to the game manager
    }
}

