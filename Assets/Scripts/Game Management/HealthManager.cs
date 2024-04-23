using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    private List<GameObject> cropList = new List<GameObject>();

    void Awake() 
    {

        cropList.Clear();

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Crop").Length; i++) 
        {
            cropList.Add(GameObject.FindGameObjectsWithTag("Crop")[i]);
        }
    }

    // Method to remove a crop from the list
    public void RemoveCrop(GameObject crop)
    {
        cropList.Remove(crop);
    }

    // Method to get a random crop from the list
    public GameObject GetRandomCrop()
    {
        if (cropList.Count > 0)
        {
            int randomIndex = Random.Range(0, cropList.Count);
            return cropList[randomIndex];
        }
        else
        {
            return null; // Return null if the list is empty
        }
    }
}