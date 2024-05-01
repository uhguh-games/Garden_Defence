using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public List<GameObject> cropList = new List<GameObject>();
    [SerializeField] private List<GameObject> cropInventory = new List<GameObject>();
    void Awake() 
    {
        cropList.Clear();

        PopulateCropList();
    }

    public void PopulateCropList() 
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Crop").Length; i++) 
        {
            cropList.Add(GameObject.FindGameObjectsWithTag("Crop")[i]);
            cropInventory.Add(GameObject.FindGameObjectsWithTag("Crop")[i]);
        }
    }

    /*

    public void PopulateCropList()
    {
        GameObject[] crops = GameObject.FindGameObjectsWithTag("Crop");

        foreach (GameObject crop in crops) 
        {
            cropList.Add(crop);
            cropInventory.Add(crop);
        }
    }

    */

    public void RePopulateCropList() 
    {
        cropList.Clear();

        foreach(GameObject crop in cropInventory) 
        {
            cropList.Add(crop);
        }
    }

    public void RemoveCrop(GameObject crop)
    {
        cropList.Remove(crop);
    }

    public void ResetHealth() // make this through events
    {
        // Remove all crops from the scene and replace with a new "batch"
        StartCoroutine(ResetHealthCoroutine(2f));
    }

    IEnumerator ResetHealthCoroutine(float waitTime) 
    {
        while (true) 
        {
            foreach (GameObject cropObject in cropInventory)
            {
                cropObject.SetActive(false);
            }

            yield return new WaitForSeconds(waitTime);

            foreach (GameObject cropObject in cropInventory)
            {
                cropObject.SetActive(true);
            }

            yield break;
        }
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