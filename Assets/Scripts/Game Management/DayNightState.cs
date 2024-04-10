using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public enum TimeState
{
    Morning,
    Day,
    Evening,
    Night
}

public class DayNightState : MonoBehaviour
{
    public TimeState currentState;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timeCounterText;
    [SerializeField] private float gameTime;
    [SerializeField] private List<GameObject> fireFlies = new List<GameObject>();
    private LightCycle lightCycle;

    void Awake() 
    {
        lightCycle = GameObject.Find("Directional Light").GetComponent<LightCycle>();
    }

    void Update() 
    {
        GameTimer();
        // Just for testing

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            ChangeState(TimeState.Morning);
            lightCycle.StartLightRoutine(lightCycle.morningRotation);

            foreach (GameObject particleEffect in fireFlies) 
            {
                particleEffect.SetActive(false);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            ChangeState(TimeState.Day);
            lightCycle.StartLightRoutine(lightCycle.dayRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            ChangeState(TimeState.Evening);
            lightCycle.StartLightRoutine(lightCycle.eveningRotation);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            ChangeState(TimeState.Night);
            lightCycle.StartLightRoutine(lightCycle.nightRotation);

            foreach (GameObject particleEffect in fireFlies) 
            {
                particleEffect.SetActive(true);
            }
        }
    }

    public void GameTimer() 
    {
        gameTime++;
        timeCounterText.text = $"{gameTime}";
    
    }

    public void ChangeState(TimeState newState)
    {
        // Add any additional logic you need when changing states
        currentState = newState;

        timeText.text = $"Time of day: {currentState}";

        switch (currentState)
        {
            case TimeState.Morning:
                // Lighting
                // Spawns
                break;

            case TimeState.Day:
                //
                break;

            case TimeState.Evening:
                //
                break;

            case TimeState.Night:
                // Turn on night conditions

                break;
        }
    }
}