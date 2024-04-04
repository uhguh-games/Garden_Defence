using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
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

    // private GameObject selectedItem;
    public GameObject itemToPlace;
    private ResourceControls resourceControls;
    private HexGrid hexGrid;

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
        resourceControls = GetComponent<ResourceControls>();
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
            // Debug.Log(selectedItem.GetComponent<Slot>().ItemInSlot.itemName);
            itemToPlace = selectedItem.GetComponent<Slot>().ItemInSlot.prefab;
        }

        if (itemToPlace.tag == "Tower") 
        {
            towerSpawner.PreviewTower();
        } 
        else if (itemToPlace.tag == "Resource") 
        {
            towerSpawner.PreviewTower();
        }

        // Show tooltip
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        // Hide tooltip
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (itemToPlace.tag == "Tower") 
        {
            Debug.Log($"Tower object {itemToPlace.name} is placed");
            towerSpawner.PlaceTower();
        } 
        else if (itemToPlace.tag == "Resource") 
        {
            Debug.Log($"Resource object {itemToPlace.name} is placed");
            towerSpawner.PlaceItem();
        }
    }
}
