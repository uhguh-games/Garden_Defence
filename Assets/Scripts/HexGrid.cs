using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private Transform hexSprite;

    private GridHexXZ<GridObject> gridHexXZ;
    private GridObject lastGridObject;

    public Vector3 lastWorldPosition;

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
                Transform visualTransform = Instantiate(hexSprite, gridHexXZ.GetWorldPosition(x, z), Quaternion.identity);
                visualTransform.transform.parent = hexGridContainer.transform;
                gridHexXZ.GetGridObject(x, z).visualTransform = visualTransform;
                gridHexXZ.GetGridObject(x, z).Hide();
            }
        }
        
        ToggleGridVisibility(false);
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
            // Print the world position of the current cell
            int x, z;
            gridHexXZ.GetXZ(lastGridObject.visualTransform.position, out x, out z);
            lastWorldPosition = gridHexXZ.GetWorldPosition(x, z);
            // Debug.Log("World Position of Current Cell: " + lastWorldPosition);

            lastGridObject.Show();
        }
    }

    public void ToggleGridVisibility(bool isVisible)
    {
        hexGridContainer.SetActive(isVisible);
    }
}
