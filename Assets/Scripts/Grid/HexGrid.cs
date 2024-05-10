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
    public bool insideBounds;

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

        hexGridContainer = GameObject.Find("HexGridContainer");
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
            // Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }

    private void Update()
    {
        if (lastGridObject != null)
        {
            lastGridObject.Hide();
        }

        if (insideBounds) 
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
                lastGridObject.Show("Unavailable");
                canPlace = false;
            } 
            else 
            {
                lastGridObject.Show("Selected");
                canPlace = true;
            }
        }
    }
    public void FindFirePitsAndDoStuff() // refactor These two methods to work together
    {
        foreach (var kvp in ItemsInScene) 
        {
            if (kvp.Key.name == "FirePit(Clone)") 
            {
                Vector3 position = kvp.Value;
                GameObject firePit = kvp.Key;
            
                Outline outline = firePit.GetComponent<Outline>();
                  
                if (outline != null) 
                {
                    outline.ToggleOutlineOn(true);
                }
            }
        }
    }

    public void TurnOffFirePitOutline() // refactor repetitive code later
    {
        print ("should fire");
        foreach (var kvp in ItemsInScene) 
        {
            if (kvp.Key.name == "FirePit(Clone)") 
            {
                Vector3 position = kvp.Value;
                GameObject firePit = kvp.Key;
            
                Outline outline = firePit.GetComponent<Outline>();
                outline.ToggleOutline(false);
            }
        }
    }


    public void FindItemInList()
    {
        foreach (var kvp in ItemsInScene) 
        {
            if (kvp.Key.name == "FirePit(Clone)") 
            {
                Vector3 position = kvp.Value;
          
                if (currentWorldPosition == position) 
                {
                    // Debug.Log($"Position match found. Current World Position = {currentWorldPosition} Fire Pit position = {position}");
                 
                    GameObject firePit = kvp.Key;
            
                    FirePit firePitScript = firePit.GetComponent<FirePit>();
                  
                    if (firePitScript != null) 
                    {
                        firePitScript.ReActivateFire();
                    }
                } 
            }
        }
    }

    public void ClearObjectsInScene() 
    {
        occupiedPositions.Clear();

        foreach (var kvp in ItemsInScene) 
        {
            GameObject item = kvp.Key;
            Vector3 itemPosition = kvp.Value;

            occupiedPositions.Remove(itemPosition);
            Destroy(item);
        }
        
        ItemsInScene.Clear();
    }

    public void ToggleGridVisibility(bool isVisible)
    {
        hexGridContainer.SetActive(isVisible);
    }
}
