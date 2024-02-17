using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;

    // Write any code needed for UI regarding to the grid in HexGrid

    Cannon towerIndicator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Cannon basicTower;
    private bool spawnerActive;
    Vector3 worldPosition;

    [Header("Grid")]
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;


    private void Awake() 
    {
        spawnerActive = false;

        hexGrid = FindObjectOfType<HexGrid>();
    }

    private void Update() 
    {
        if (hexGrid != null)
        {
            worldPosition = hexGrid.lastWorldPosition;
        }

         if (Input.GetKeyDown(KeyCode.Alpha0) && !spawnerActive) 
        {
            towerIndicator = Instantiate(basicTower, GetMousePosition(), Quaternion.identity);
            spawnerActive = true;
        }

        if (spawnerActive) 
        {
            towerIndicator.transform.position = GetMousePosition();

            if (Input.GetMouseButton(0)) 
            {
                towerIndicator = null;
                spawnerActive = false;
            }
            else if (Input.GetMouseButton(1)) 
            {
                Destroy(towerIndicator.gameObject);
                spawnerActive = false;
            }
        }
    }

    private Vector3 GetMousePosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);

            // Convert raycast hit point to a position on the tilemap grid
            cellPosition = tilemap.WorldToCell(hit.point);

            // Calculate the world position of the center of the hexagon
            Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);

            return worldPosition;
        }

        return Vector3.zero;
    }
}
