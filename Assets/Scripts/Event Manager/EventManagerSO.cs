using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventManager", menuName = "Create new Manager/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action junkCollect;
    public event Action allEnemiesKilled;
    public event Action onCropEaten;
    public event Action winCondition;
    public event Action loseCondition;
    public event Action addEnemy;
    public event Action onKill;

    public void OnKill()
    {
        onKill?.Invoke();
    }
    public void LootCollected()
    {
        junkCollect?.Invoke();
    }

    public void CropEaten()
    {
        onCropEaten?.Invoke();
    }

    public void EnemySummation()
    {
        addEnemy?.Invoke();
    }

    public void EnemiesKilled()
    {
        allEnemiesKilled?.Invoke();
    }

    public void Win()
    {
        winCondition?.Invoke();
    }
    public void Lose()
    {
        loseCondition?.Invoke();
    }
}