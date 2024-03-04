using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private HexGrid hexGrid;
    private PlaceItem placeItem;
    [SerializeField] LayerMask groundLayer;
    private bool spawnerActive;

    // Cannon towerIndicator;
    GameObject towerIndicator;
    Vector3 worldPosition;

    [Header("Grid")]
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;


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
            worldPosition = hexGrid.lastWorldPosition;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0) && !spawnerActive) 
        {
            PreviewTower();
        }

        if (spawnerActive) 
        {
            towerIndicator.transform.position = GetMousePosition();

            if (Input.GetMouseButtonDown(0)) 
            {
                PlaceTower();
            }
            else if (Input.GetMouseButtonDown(1)) 
            {
                CancelTower();
            }
        }
    }

    public void PreviewTower() 
    {
        towerIndicator = Instantiate(placeItem.itemToPlace, GetMousePosition(), Quaternion.identity);
        hexGrid.ToggleGridVisibility(true);
        spawnerActive = true;
    }

    public void PlaceTower() 
    {
        towerIndicator.GetComponent<Tower>().ActivateTower();
        hexGrid.ToggleGridVisibility(false);
        towerIndicator = null;
        spawnerActive = false;
    }

    public void CancelTower() 
    {
        Destroy(towerIndicator.gameObject);
        spawnerActive = false;
    }

    private Vector3 GetMousePosition() // Later: Fetch mouse position from mouse3D script?
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);

            cellPosition = tilemap.WorldToCell(hit.point);

            Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);

            return worldPosition;
        }

        return Vector3.zero;
    }
}
