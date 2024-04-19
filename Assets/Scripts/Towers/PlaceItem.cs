using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// This script handles the tower placement CONTROLS using the EventSystem

[RequireComponent(typeof(GraphicRaycaster))]
public class PlaceItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    private TowerSpawner towerSpawner;
    private Slot slot;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    EconomyManager economyManager;

    // private GameObject selectedItem;
    public GameObject itemToPlace = null;
    private HexGrid hexGrid;
    public int currentCost; // should be set by the economy manager
    [SerializeField] TextMeshProUGUI tooltipText; // I will move this to a different class later :)
    

    void Start() 
    {
        hexGrid = FindObjectOfType<HexGrid>();

        if (GetComponent<GraphicRaycaster>() == null) 
        {
            GraphicRaycaster m_Raycaster = gameObject.AddComponent(typeof(GraphicRaycaster)) as GraphicRaycaster;
        } 
        else 
        {
            m_Raycaster = GetComponent<GraphicRaycaster>();
        }

        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
        m_EventSystem = GetComponent<EventSystem>();
    }

   public void OnPointerClick(PointerEventData eventData) 
   {
        // Show tooltip
   }

    public void OnPointerDown(PointerEventData eventData) 
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject selectedItem = result.gameObject;
            Slot slot = selectedItem.GetComponent<Slot>();

            if (slot != null) 
            {
                itemToPlace = slot.ItemInSlot.prefab;
                // currentCost = slot.ItemInSlot.value;
                currentCost = economyManager.GetItemValue(slot.ItemInSlot.itemName);

                // Debug.Log($"{slot.ItemInSlot.name} costs {slot.ItemInSlot.value}");
                //itemToPlace = selectedItem.GetComponent<Slot>().ItemInSlot.prefab;
                break;
            }
        }

        if (economyManager.playersJunk >= currentCost) 
        {
            towerSpawner.PreviewTower();
            Invoke("TextAway", 2.5f);
        } 
        else 
        {
            tooltipText.text =  "Not enough funds.";
            Invoke("TextAway", 2.5f);
        }

        // Show tooltip
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        // Hide tooltip
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (itemToPlace != null) 
        {
            if (itemToPlace.tag == "Tower") 
            {
                towerSpawner.PlaceTower();
            }
            else if (itemToPlace.tag == "Resource")
            {
                towerSpawner.PlaceItem();
            }
            
            itemToPlace = null;
        }
    }
    public void TextAway() // will be moved later
    {
        tooltipText.text = " ";
    }
}
