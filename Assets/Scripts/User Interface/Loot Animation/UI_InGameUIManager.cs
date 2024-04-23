using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_InGameUIManager : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;

    private List<UI_ItemSlot> ui_ItemSlots;
    private Canvas canvas;
    private UI_CollectedItemAnimSpawner collectableItem; // rename this

    private void OnEnable()
    {
        eventManager.onLootCollected += PlayPulseAnimation;
    }

    private void OnDisable()
    {
        eventManager.onLootCollected -= PlayPulseAnimation;
    }

    private void Awake()
    {
        collectableItem = GetComponent<UI_CollectedItemAnimSpawner>();

        ui_ItemSlots = new List<UI_ItemSlot>();
        ui_ItemSlots.AddRange(GetComponentsInChildren<UI_ItemSlot>());

        foreach (var item in ui_ItemSlots)
        {
            // Debug.Log($"UI Item initialized with collectable type {item.CollectableType}");
        }

        canvas = GetComponent<Canvas>();
    }

    private void PlayPulseAnimation(Collectable loot)
    {
        var collectableType = loot.CollectableType;

        foreach (var slot in ui_ItemSlots)
        {
            if (slot.CollectableType == collectableType)
            {
                StartCoroutine(collectableItem.SpawnUICollectableItems(canvas, loot, slot));
            }
        }
    }
}
