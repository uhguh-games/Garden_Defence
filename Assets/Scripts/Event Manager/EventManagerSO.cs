using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventManager", menuName = "Create new Manager/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action onKill;

    public void LootCollected()
    {
        onKill?.Invoke();
    }
}