using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    private Transform deathPos;
    public float maxHealth = 10f;
    public int eatingTime;
    public float currentHealth;
    private HealthBar healthBar;
    [SerializeField] Transform hitTarget; // empty object on the enemy
    private PoolManager poolManager;
    // private Crop cropToEat;
    private int enemyJunkValue; // set by the economy manager
    EconomyManager economyManager;
    GameManager gameManager;
    public bool enemyHungry = true;

    [Tooltip("Scriptable Object of the enemy")]
    [SerializeField] EnemyScriptableObject enemyStats;
    private EnemyType enemyType;
    private EnemyMovement enemyMovement;
    Material originalMaterial;
    Material whiteMaterial;
    EnemyTracker enemyTracker;

    private void Awake()
    {
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    void Start()
    {
        enemyType = enemyStats.Type;
        originalMaterial = GetComponent<Renderer>().material;
        whiteMaterial = Resources.Load("Materials/EnemyHit") as Material;
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
            enemyMovement.OnEnemyDeath();
            eventManager.OnKill();
            DropJunk();
            this.gameObject.SetActive(false); // Enemy gets returned into the pool
        }

        if (gameManager.levelWon || gameManager.levelLost) 
        {
            this.gameObject.SetActive(false);
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
        GetComponent<MeshRenderer>().material = whiteMaterial;

        float percentage = damage / maxHealth;
        float actualDamage = maxHealth * percentage;
    
        currentHealth -= actualDamage;
        healthBar.SetHealth(currentHealth);

        // Invoke("ResetMat", 0.10f);
    
    }

    void ResetMat()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
        print ("Material is supposed to change");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Temp") 
        {
            this.gameObject.SetActive(false); // Enemy gets returned into its' pool
            eventManager.OnKill();
            enemyMovement.cropToEat = null;
        }
        
        if (enemyHungry && other.tag == "Crop")
        { 
            // cropToEat = other.GetComponent<Crop>();
            StartCoroutine(EatRoutine());
            enemyHungry = false;
        }
    }

    IEnumerator EatRoutine() //eat for n seconds
    {
        yield return new WaitForSeconds(eatingTime);

        Invoke("Eat", 1.6f); //tell crop to delete itself and to add the points to the game manager

        yield return new WaitForSeconds(eatingTime);
        enemyMovement.WalkOffScreen(); // walk off screen and despawn
        yield break;
    }

    public void Eat() 
    {
        enemyMovement.cropToEat.GetComponent<Crop>().GetEaten();
    }
}

