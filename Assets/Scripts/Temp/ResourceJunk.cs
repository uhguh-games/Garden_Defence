using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceJunk : AutoDestroyPoolableObject
{
    [SerializeField] EventManagerSO eventManager;
    private GameObject mouseObj;
    private LootCollectionUI lootCollectionUI;
    


    private void Awake()
    {
        mouseObj = GameObject.Find("Mouse3D");
        lootCollectionUI = GameObject.Find("Main Canvas").GetComponent<LootCollectionUI>();
       
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
            lootCollectionUI.Reset(this.transform);
            lootCollectionUI.RewardGoldStack(10);
            Debug.Log("Collected junk");
            eventManager.LootCollected();
            this.gameObject.SetActive(false); // loot object returns to the pool

        }
        Debug.Log(this.transform.position);
    }
}
