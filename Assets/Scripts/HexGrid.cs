using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour 
{
    [SerializeField] private Transform pfHex;

    private GridHexXZ<GridObject> gridHexXZ;
    private GridObject lastGridObject;

    [SerializeField] int horizontalCellAmount = 10;
    [SerializeField] int verticalCellAmount = 10;

    private GameObject hexGridContainer;

    private class GridObject 
    {
        public Transform visualTransform;

        public void Show() 
        {
            visualTransform.Find("Selected").gameObject.SetActive(true);
        }

        public void Hide() 
        {
            visualTransform.Find("Selected").gameObject.SetActive(false);
        }
    }

    private void Awake() 
    {
        int width = horizontalCellAmount;
        int height = verticalCellAmount;
        float cellSize = 2f;

        hexGridContainer = GameObject.Find("HexGrid");

        gridHexXZ = new GridHexXZ<GridObject>(width, height, cellSize, Vector3.zero, (GridHexXZ<GridObject> g, int x, int y) => new GridObject());

        for (int x = 0; x < width; x++) 
        {
            for (int z = 0; z < height; z++) 
            {
                Transform visualTransform = Instantiate(pfHex, gridHexXZ.GetWorldPosition(x, z), Quaternion.identity);
                visualTransform.transform.parent = hexGridContainer.transform;
                gridHexXZ.GetGridObject(x, z).visualTransform = visualTransform;
                gridHexXZ.GetGridObject(x, z).Hide();
            }
        }
    }

    private void Update() 
    {
        if (lastGridObject != null) 
        {
            lastGridObject.Hide();
        }

        lastGridObject = gridHexXZ.GetGridObject(Mouse3D.GetMouseWorldPosition());

        if (lastGridObject != null) 
        {
            lastGridObject.Show();
        }
    }
}