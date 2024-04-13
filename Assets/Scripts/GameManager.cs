using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    public int junk = 0;
    [SerializeField] private TextMeshProUGUI textybit; 

    public float cropHealth;
    private float baseCropHealth;
    private int numberOfCrops;
    public int junkAmount = 10;
    private int enemiesInScene;
    private int kills;

    private void Update()
    {
        if (cropHealth < 0)
        { 
            cropHealth= 0; 
        }
        
        if (kills == enemiesInScene)
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
        eventManager.junkCollect += CollectJunk;
        eventManager.onCropEaten += DamageCrops;
        eventManager.addEnemy += EnemySummation;
        eventManager.onKill += KillCounter;
    }
    private void OnDisable()
    {
        eventManager.onKill -= CollectJunk;
    }

    private void CollectJunk()
    {
        junk += junkAmount;
    }

    private void DamageCrops()
    {
        cropHealth -= (baseCropHealth/numberOfCrops);
       
    }

    private void EnemySummation() 
    {
        enemiesInScene++;
    }
    private void KillCounter()
    {
        kills++;
    }

    private void Win()
    {
    
    }
    private void Lose()
    {
        textybit.enabled = true;
        textybit.text = "You lose";
        textybit.color = Color.red;
    }


}
