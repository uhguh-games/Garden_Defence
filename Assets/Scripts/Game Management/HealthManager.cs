using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public List<GameObject> cropList = new List<GameObject>();
    void Awake() 
    {

        cropList.Clear();

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Crop").Length; i++) 
        {
            cropList.Add(GameObject.FindGameObjectsWithTag("Crop")[i]);
        }
    }
    public void RemoveCrop(GameObject crop)
    {
        cropList.Remove(crop);
    }

    public void AddCrop(GameObject crop) 
    {
        cropList.Add(crop);
    }

    public GameObject GetRandomCrop()
    {
        if (cropList.Count > 0)
        {
            int randomIndex = Random.Range(0, cropList.Count);
            return cropList[randomIndex];
        }
        else
        {
            return null; 
        }
    }
}