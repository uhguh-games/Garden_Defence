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
    [SerializeField] LayerMask collectableLayers;
    private Camera cam;
    EconomyManager economyManager;
    
    public int junkValue = 0;

    private void Awake()
    {
        cam = Camera.main;
        mouseObj = GameObject.Find("Mouse3D");
        targetPosition = GameObject.Find("lootTarget").transform;
        economyManager = FindObjectOfType<EconomyManager>();
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {   
            // old animation
            // GameObject lootImageInstance = Instantiate(lootCanvas, this.transform.position, Quaternion.identity); // animation UI object
            // LootAnimation animation = lootImageInstance.GetComponent<LootAnimation>();
            // animation.Initialize(targetPosition);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, collectableLayers))
            {
                if (hit.collider.gameObject.TryGetComponent<Collectable>(out Collectable loot))
                {
                    print ("Thizzzzz workz");
                    eventManager.LootCollected(loot);
                }

            }

            eventManager.LootCollected(junkValue);

            this.gameObject.SetActive(false); // loot resource object returns to the pool
        }
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
    { /*
        if (Input.GetMouseButtonDown(0))
        {   
            // old animation
            // GameObject lootImageInstance = Instantiate(lootCanvas, this.transform.position, Quaternion.identity); // animation UI object
            // LootAnimation animation = lootImageInstance.GetComponent<LootAnimation>();
            // animation.Initialize(targetPosition);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, collectableLayers))
            {
                if (hit.collider.gameObject.TryGetComponent<Collectable>(out Collectable loot))
                {
                    eventManager.LootCollected(loot);
                }

                print ("Thizzzzz workz");
            }

            eventManager.LootCollected(junkValue);

            this.gameObject.SetActive(false); // loot resource object returns to the pool
        }
        */
    }
}
