using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceJunk : AutoDestroyPoolableObject
{
    [SerializeField] EventManagerSO eventManager;
    private GameObject mouseObj;
    [SerializeField] private GameObject lootImage;
    [SerializeField] private GameObject lootCanvas;
    [SerializeField] private Transform targetPosition;

    private void Awake()
    {
        mouseObj = GameObject.Find("Mouse3D");
        targetPosition = GameObject.Find("lootTarget").transform;
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    private void OnMouseDown()
    {
        

        if (Input.GetMouseButtonDown(0))
        {   
            GameObject lootImageInstance = Instantiate(lootCanvas, this.transform.position, Quaternion.identity);
            LootAnimation animation = lootImageInstance.GetComponent<LootAnimation>();
            animation.Initialize(targetPosition);


            // lootImageInstance.transform.SetParent(lootCanvas.transform);
            Debug.Log("Collected junk");
            eventManager.LootCollected();
            this.gameObject.SetActive(false); // loot object returns to the pool

        }
    }
}
