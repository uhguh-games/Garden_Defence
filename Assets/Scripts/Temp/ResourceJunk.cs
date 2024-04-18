using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceJunk : AutoDestroyPoolableObject
{
    [SerializeField] EventManagerSO eventManager;
    private GameObject mouseObj;
    [SerializeField] private GameObject lootCanvas;
    [SerializeField] private Transform targetPosition;
    EconomyManager economyManager;
    
    public int junkValue = 0;

    private void Awake()
    {
        mouseObj = GameObject.Find("Mouse3D");
        targetPosition = GameObject.Find("lootTarget").transform;
        economyManager = FindObjectOfType<EconomyManager>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public void SetJunkValue(int amount) 
    {
        junkValue += amount;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            GameObject lootImageInstance = Instantiate(lootCanvas, this.transform.position, Quaternion.identity);
            LootAnimation animation = lootImageInstance.GetComponent<LootAnimation>();
            animation.Initialize(targetPosition);

            eventManager.LootCollected(junkValue);
            this.gameObject.SetActive(false); // loot object returns to the pool

        }
    }
}
