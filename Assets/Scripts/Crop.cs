using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    private MeshRenderer render;
    private Collider col;

    private void Start()
    {
        render = this.GetComponent<MeshRenderer>();
        col = this.GetComponent<BoxCollider>();
    }

    public void GetEaten()
    {
        eventManager.CropEaten();
        eventManager.PlaySFX(0);
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
