using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;


    public void GetEaten()
    {
        Destroy(this.gameObject); //simply delete itself once monster's eatingTime has elapsed
        eventManager.CropEaten();
    }
}

