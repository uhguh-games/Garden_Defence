using UnityEngine;

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

    public void ChangeState(TimeState newState)
    {
        // Add any additional logic you need when changing states
        currentState = newState;

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