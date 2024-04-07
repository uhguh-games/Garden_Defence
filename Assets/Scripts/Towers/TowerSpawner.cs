using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

// This script handles tower/item placement LOGIC

public class TowerSpawner : MonoBehaviour
{
    private PlaceItem placeItem;
    [SerializeField] LayerMask groundLayer;
    private bool spawnerActive;
    public GameObject towerIndicator;
    Vector3 worldPosition;
    public bool spaceBlocked;

    [Header("Grid")]
    private HexGrid hexGrid;
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;

    [Header("UI & UX")]
    [SerializeField] TextMeshProUGUI tooltipText; // I will move this to a different class later :)

    private void Awake() 
    {
        spawnerActive = false;
        hexGrid = FindObjectOfType<HexGrid>();
        placeItem = GameObject.Find("Main Canvas").GetComponent<PlaceItem>();
    }

    private void FixedUpdate() 
    {
        if (hexGrid != null)
        {
            worldPosition = hexGrid.currentWorldPosition;
        }
      
        if (spawnerActive) 
        {
            towerIndicator.transform.position = GetMousePosition();
        }

        hexGrid.GetLitEnemies(); // Why am I calling this here?
    }

    public void PreviewTower()
    {
        towerIndicator = Instantiate(placeItem.itemToPlace, GetMousePosition(), Quaternion.identity);
        hexGrid.ToggleGridVisibility(true);
        spawnerActive = true;
    }

    public void PlaceTower() 
    {
        if (hexGrid.canPlace && !spaceBlocked) 
        {
            towerIndicator.GetComponent<Tower>().ActivateTower();
            hexGrid.UpdatePositionList();
            hexGrid.UpdatePositionDictionary(towerIndicator);
            towerIndicator = null;
            spawnerActive = false;
        }
        else 
        {
            tooltipText.text = "Can't place here";
            CancelTower();
            Invoke("TextAway", 2.5f);
        }
        
        hexGrid.ToggleGridVisibility(false);
    }

    public void PlaceItem() 
    { 
        if (spawnerActive)
        {
            CancelTower();
        }

        hexGrid.FindItemInList(); // Scans through the towers placed in the scene. If a firepit is found and the current mouse pos matches with the fire pits pos the firepit is reignited
        hexGrid.ToggleGridVisibility(false);

    }

    public void TextAway() // will be moved later
    {
        tooltipText.text = " ";
    }

    public void CancelTower() 
    {
        Destroy(towerIndicator.gameObject);
        spawnerActive = false;
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green);

            cellPosition = tilemap.WorldToCell(hit.point);

            if (cellPosition.x >= 0 && cellPosition.x < hexGrid.Width && cellPosition.y >= 0 && cellPosition.y < hexGrid.Height) // is current position of the mouse within the grid bounds
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);
                hexGrid.testFlag = false; // rename "testflag" (•‿•)
                return worldPosition;
            } 
            else 
            {
                hexGrid.testFlag = true;
                // turn towerIndicator red 
                return hit.point;
            }
        }
        
        return Vector3.zero;
    }
}
