using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceControls : MonoBehaviour
{
    private Vector3 mousePosition;
    private PlaceItem placeItem;
    private GameObject itemIndicator;
    private bool spawnerActive;

    void Start() 
    {
        placeItem = GameObject.Find("Main Canvas").GetComponent<PlaceItem>();
        mousePosition = Mouse3D.GetMouseWorldPosition();
    }
    private void FixedUpdate() 
    {
        if (spawnerActive) 
        {
            itemIndicator.transform.position = mousePosition;
        }
    }

    public void PreviewItem()
    {
        spawnerActive = true;
        itemIndicator = Instantiate(placeItem.itemToPlace, mousePosition, Quaternion.identity);
        // hexGrid.ToggleGridVisibility(true);
        // spawnerActive = true;
    }
   public void PlaceResource()
   {
        itemIndicator = null;
        spawnerActive = false;

        /*
        if () 
        {
            // towerIndicator.GetComponent<Tower>().ActivateTower();
            // hexGrid.UpdatePositionList();
            // towerIndicator = null;
            // spawnerActive = false;
        }
        else 
        {
            // tooltipText.text = "Can't place here";
            // CancelTower();
            // Invoke("TextAway", 2.5f);
        }
        
        // hexGrid.ToggleGridVisibility(false);
    */
    }

}
