using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventManager", menuName = "Managers/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action onKill;

    public void LootCollected()
    {
        onKill?.Invoke();
    }
}