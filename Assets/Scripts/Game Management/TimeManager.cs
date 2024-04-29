using UnityEngine;
using UnityEngine.UI;
using System;
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

public class TimeManager : MonoBehaviour
{
    public event Action<TimeState> OnTimeStateChanged;
    public TimeState currentState;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timeCounterText;
    [SerializeField] float gameTime;
    public bool timeIsUp = false;
    public bool timeGo = true;
    public float maxGameDuration;

    // will be set by level manager
    public float morningDuration;
    public float dayDuration;
    public float eveningDuration;
    public float nightDuration;

    LevelManager levelManager;

    [Range(1, 4)]
    [SerializeField] int playSpeed = 1;

    [SerializeField] List<GameObject> fireFlies = new List<GameObject>();

    private LightCycle lightCycle;

    void Awake() 
    {   
        lightCycle = GameObject.Find("Directional Light").GetComponent<LightCycle>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update() 
    {
        // playback speed for debugging
        Time.timeScale = playSpeed;

        if (timeGo) 
        {
            if (gameTime < maxGameDuration)
            {
                GameTimer();
                TimeState nextState = CalculateState(gameTime);
                
                if (nextState != currentState)
                {
                    ChangeState(nextState);
                }
            } 
            else 
            {
                gameTime = maxGameDuration;
            }
        }

        if (gameTime >= maxGameDuration) 
        {
            timeIsUp = true;
            timeGo = false;
            gameTime = maxGameDuration;
        }
    }

    TimeState CalculateState(float time)
    {
        if (time < morningDuration)
        {
            return TimeState.Morning;
        }
        else if (time < morningDuration + dayDuration)
        {
            return TimeState.Day;
        }
        else if (time < morningDuration + dayDuration + eveningDuration)
        {
            return TimeState.Evening;
        }
        else
        {
            return TimeState.Night;
        }
    }

    public void SetTimeOfDay(TimeState newTimeOfDay)
    {
        currentState = newTimeOfDay;
        OnTimeStateChanged?.Invoke(currentState);
    }

    public void GameTimer() 
    {
        gameTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        timeCounterText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds:000}";
    }

    public void StopTimer() 
    {
        timeGo = false;
        timeIsUp = true;
    }

    public void ChangeState(TimeState newState)
    {
        currentState = newState;

        timeText.text = $"Time of day: {currentState}";

        switch (currentState)
        {
            case TimeState.Morning:
                // Lighting
            
                lightCycle.StartLightRoutine(lightCycle.morningRotation);

                foreach (GameObject particleEffect in fireFlies)
                {
                    particleEffect.SetActive(false);
                }
                break;

            case TimeState.Day:
                
                lightCycle.StartLightRoutine(lightCycle.dayRotation);
                break;

            case TimeState.Evening:

                lightCycle.StartLightRoutine(lightCycle.eveningRotation);
                break;

            case TimeState.Night:
                
                lightCycle.StartLightRoutine(lightCycle.nightRotation);
                                
                foreach (GameObject particleEffect in fireFlies)
                {
                    particleEffect.SetActive(true);
                }
                
                break;
        }

        OnTimeStateChanged?.Invoke(currentState);
    }
}