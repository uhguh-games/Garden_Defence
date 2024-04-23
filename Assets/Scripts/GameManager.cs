using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    // public int junk = 5;
    [SerializeField] private TextMeshProUGUI textybit; 
    public float cropHealth;
    private float baseCropHealth;
    private int numberOfCrops;
    private int enemiesInScene;
    private int kills;

    private TimeManager timeManager;

    void Start() 
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    private void Update()
    {
        // we could separate this logic into a separate state machine

        if (cropHealth < 0)
        { 
            cropHealth= 0; 
        }
        
        if (timeManager.timeIsUp && cropHealth > 0) // win condition would be something like: timer is out and cropHealth < 0
        {
            Win();
        }
        if (cropHealth == 0)
        {
            Lose();
        }
    }
    private void Awake()
    {
        baseCropHealth = cropHealth;
        numberOfCrops = GameObject.FindGameObjectsWithTag("Crop").Length;
    }
    private void OnEnable()
    {
        // eventManager.junkCollect += CollectJunk;
        eventManager.onCropEaten += DamageCrops;
        eventManager.addEnemy += EnemySummation; // not sure this is needed
        eventManager.onKill += KillCounter;
    }

    /*
    private void OnDisable()
    {
        eventManager.onKill -= CollectJunk;
    }

    /*
    private void CollectJunk()
    {
        junk += junkAmount;
        economyManager.AddJunk(junkAmount);
    }
    */

    private void DamageCrops()
    {
        cropHealth -= (baseCropHealth/numberOfCrops);
    }

    private void EnemySummation() // not sure this is needed
    {
        // enemiesInScene++;
    }
    private void KillCounter()
    {
        // kills++;
    }

    private void Win()
    {
        textybit.enabled = true;
        textybit.text = "You win";
        textybit.color = Color.green;
    }
    private void Lose()
    {
        textybit.enabled = true;
        textybit.text = "You lose";
        textybit.color = Color.red;
    }
}
