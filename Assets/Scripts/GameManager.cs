using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    public int junk = 0;

    public int cropHealth;
    [SerializeField] private int damageToCrops;
    public int junkAmount = 10;

    private void OnEnable()
    {
        eventManager.onKill += CollectJunk;
        eventManager.onCropEaten += DamageCrops;
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
        cropHealth -= damageToCrops;
    }

}
