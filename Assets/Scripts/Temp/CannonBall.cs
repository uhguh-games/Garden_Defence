using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : AutoDestroyPoolableObject
{
    /*
    This is a script used for testing - it will be deleted later
    */

    [SerializeField] float cannonSpeed = 5f;
    [SerializeField] float cannonDamage = 1f;
    [SerializeField] private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null) 
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }
    private void Update()
    {
        
    }

    public override void OnEnable() 
    {
        base.OnEnable();
        transform.Translate(Vector3.forward * Time.deltaTime * cannonSpeed);
    }

    public override void OnDisable() 
    {
        base.OnDisable();
    }
    
    private void DeleteCannonBall() 
    {
        // sound effect
        // vfx
        base.OnDisable();
    }


    private void OnTriggerEnter(Collider other) 
    {
        Monster monster = other.GetComponent<Monster>();

        if (other.tag == "Monster") 
        {
            if (monster != null) 
            {
                monster.TakeDamage(cannonDamage);
            }

            DeleteCannonBall();
        }
    }
}
