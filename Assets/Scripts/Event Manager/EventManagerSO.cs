using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventManager", menuName = "Create new Manager/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action<int> junkCollect;
    public event Action allEnemiesKilled;
    public event Action onCropEaten;
    public event Action winCondition;
    public event Action loseCondition;
    public event Action addEnemy;
    public event Action onKill;
    public event Action<Collectable> onLootCollected; // loot animation
    public void LootCollected(Collectable collectable) => onLootCollected?.Invoke(collectable); // loot animation

    public void OnKill()
    {
        onKill?.Invoke();
    }
    public void LootCollected(int amount)
    {
        junkCollect?.Invoke(amount);
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