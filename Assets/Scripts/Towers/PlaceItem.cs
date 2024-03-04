using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(GraphicRaycaster))]
public class PlaceItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    private TowerSpawner towerSpawner;
    private Slot slot;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem; // rename
    // private GameObject selectedItem;
    public GameObject itemToPlace;


    void Start() 
    {
        if (GetComponent<GraphicRaycaster>() == null) 
        {
            GraphicRaycaster m_Raycaster = gameObject.AddComponent(typeof(GraphicRaycaster)) as GraphicRaycaster;
        } 
        else 
        {
            m_Raycaster = GetComponent<GraphicRaycaster>();
        }

        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        m_EventSystem = GetComponent<EventSystem>();
    }

   public void OnPointerClick(PointerEventData eventData) 
   {
        // Show tooltip
        // Spawn tooltip bubble at clicked position
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
            Debug.Log(selectedItem.GetComponent<Slot>().ItemInSlot.itemName);
            itemToPlace = selectedItem.GetComponent<Slot>().ItemInSlot.prefab;
        }

        towerSpawner.PreviewTower();

        // Show tooltip
        // Spawn tooltip bubble at clicked position
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        // Hide tooltip
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        towerSpawner.PlaceTower();
        // Hide tooltip
    }
}
