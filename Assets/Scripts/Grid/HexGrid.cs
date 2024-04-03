using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class HexGrid : MonoBehaviour
{
    [SerializeField] int horizontalCellAmount = 10;
    [SerializeField] int verticalCellAmount = 10;

    public int Width { get { return horizontalCellAmount; } }
    public int Height { get { return verticalCellAmount; } }

    [SerializeField] Transform hexSprite;
    private GridHexXZ<GridObject> gridHexXZ;
    private GridObject lastGridObject;
    private TowerSpawner towerSpawner;
    public Vector3 currentWorldPosition;
    private GameObject hexGridContainer;
    public bool currentPositionEmpty;
    public bool canPlace;
    public bool testFlag;

    [Header("Objects placed in the map")]
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    [SerializedDictionary("Object", "Position")]
    public SerializedDictionary<GameObject, Vector3> ItemsInScene = new SerializedDictionary<GameObject, Vector3>();

    private class GridObject
    {
        public Transform visualTransform;
        public void Show(string spriteName)
        {
            visualTransform.Find(spriteName).gameObject.SetActive(true);
        }

        public void Hide()
        {
            visualTransform.Find("Selected").gameObject.SetActive(false);
            visualTransform.Find("Unavailable").gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        int width = horizontalCellAmount;
        int height = verticalCellAmount;
        float cellSize = 2f;

        hexGridContainer = GameObject.Find("HexGrid");
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();

        gridHexXZ = new GridHexXZ<GridObject>(width, height, cellSize, Vector3.zero, (GridHexXZ<GridObject> g, int x, int y) => new GridObject());

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Transform visualTransform = Instantiate(hexSprite, gridHexXZ.GetWorldPosition(x, z), Quaternion.identity);
                visualTransform.transform.parent = hexGridContainer.transform;
                gridHexXZ.GetGridObject(x, z).visualTransform = visualTransform;
                gridHexXZ.GetGridObject(x, z).Hide();
            }
        }

        ToggleGridVisibility(false);
    }

    public void UpdatePositionList() 
    {
        occupiedPositions.Add(currentWorldPosition);
    }

    public void UpdatePositionDictionary(GameObject placedObject) 
    {
        ItemsInScene.Add(placedObject, currentWorldPosition);

        foreach(KeyValuePair<GameObject, Vector3> kvp in ItemsInScene)  
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }


    private void Update()
    {
        if (lastGridObject != null)
        {
            lastGridObject.Hide();
        }

        if (testFlag) 
        {
            currentPositionEmpty = true; 
            canPlace = false;
        } 
        else 
        {
            currentPositionEmpty = false;
        }

        lastGridObject = gridHexXZ.GetGridObject(Mouse3D.GetMouseWorldPosition());

        if (lastGridObject != null)
        {
            int x, z;
            gridHexXZ.GetXZ(lastGridObject.visualTransform.position, out x, out z);
            currentWorldPosition = gridHexXZ.GetWorldPosition(x, z);

            if (occupiedPositions.Contains(currentWorldPosition) || currentPositionEmpty) 
            {
                // Debug.Log("Can't place");
                lastGridObject.Show("Unavailable");
                canPlace = false;
            } 
            else 
            {
                // Debug.Log("Can place");
                lastGridObject.Show("Selected");
                canPlace = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            FindItemInList();
        }
    }

    public void FindItemInList() 
    {
        foreach (var kpv in ItemsInScene) 
        {
            if (kpv.Key.name == "FirePit(Clone)") 
            {
                Vector3 position = kpv.Value;
                Debug.Log($"FirePit found in the Dictionary. It's position is {position}");
            }
        }
    }

    public void ToggleGridVisibility(bool isVisible)
    {
        hexGridContainer.SetActive(isVisible);
    }
}
