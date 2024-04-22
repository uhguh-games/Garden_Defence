using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableTypesEnum _collectableType;
    [SerializeField] EventManagerSO _eventManager;
    [SerializeField] GameObject _UI_collectableItem;
    [SerializeField] int value = 2;

    public GameObject UI_CollectableItem => _UI_collectableItem;

    public int Value => value;

    private void OnEnable()
    {
        _eventManager.onLootCollected += Remove;
    }

    private void OnDisable()
    {
        _eventManager.onLootCollected -= Remove;
    }

    public CollectableTypesEnum CollectableType
    {
        get => _collectableType;
    }


    private void Remove(Collectable treat)
    {
        if(this == treat)
            Destroy(this.gameObject);
    }



}
