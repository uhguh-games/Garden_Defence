using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceJunk : AutoDestroyPoolableObject

{
    [SerializeField] EventManagerSO eventManager;
    private GameObject mouseObj;


    private void Awake()
    {
        mouseObj = GameObject.Find("Mouse3D");
       
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
        
        Debug.Log(Mouse3D.GetMouseWorldPosition()); 
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Collected junk");
            eventManager.LootCollected();
            Destroy(gameObject); // Instead of deleting <- Set inactive
        }
    }
}
