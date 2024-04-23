using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    [SerializeField] private TextMeshProUGUI textybit; 
    public float cropHealth;
    private float baseCropHealth;
    private int numberOfCrops;
    private int enemiesInScene;
    private int kills;
    private TimeManager timeManager;

    // should PROBABLY put this somewhere else :) but its ok for now
    [SerializeField] private GameObject winLosePanel;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private RawImage winLoseImage;
    [SerializeField] private Texture loseTexture;
    [SerializeField] private Texture winTexture;

    void Start() 
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        winLosePanel.SetActive(false);
    }

    private void Update()
    {
        // we could separate this logic into a separate state machine

        if (cropHealth < 0)
        { 
            cropHealth = 0; 
        }
        
        if (timeManager.timeIsUp && cropHealth > 0)
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
        winLosePanel.SetActive(true);
        timeManager.StopTimer();
        textybit.enabled = true;
        textybit.text = "Victory!";
        feedbackText.text = $"You made {cropHealth} Gold.";
        winLoseImage.texture = winTexture;
   
        textybit.color = Color.white;
    }
    private void Lose()
    {
        winLosePanel.SetActive(true);
        timeManager.StopTimer();
        textybit.enabled = true;
        textybit.text = "Defeat!";
        feedbackText.text = "Bugged Out!";
        winLoseImage.texture = loseTexture;
        textybit.color = Color.red;
    }
}
