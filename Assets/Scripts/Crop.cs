using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    private MeshRenderer render;
    private Collider col;

    public bool cropTaken;

    private void Start()
    {
        render = this.GetComponent<MeshRenderer>();
        col = this.GetComponent<BoxCollider>();
    }

    public void RemoveCrop() 
    {
        Debug.Log("RRRRARAAAAAA");
        cropTaken = true;
    }
    public void GetEaten()
    {
        eventManager.CropEaten();
        this.gameObject.tag = "CropEaten";
        // this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        this.enabled = false;
    }
}
