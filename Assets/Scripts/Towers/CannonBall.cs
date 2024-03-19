using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : AutoDestroyPoolableObject
{
    [SerializeField] float cannonSpeed = 5f;
    // float rotationSpeed = 360f;
    [SerializeField] float cannonDamage = 1f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float waitTime = 0.1f;
    private Rigidbody rb;
    private Monster targetedEnemy;
    private Vector3 lastDirection;
    [SerializeField] GameObject impact;
    public bool onGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    
        if (rb == null) 
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
        StartCoroutine(Spin());
    }
    private void Update()
    {
        if (targetedEnemy) // if the targeted enemy is still alive
        {
            lastDirection = (targetedEnemy.getHitTarget().position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetedEnemy.getHitTarget().position,
                    cannonSpeed * Time.deltaTime);
        }
        else if (rb.isKinematic) // Target is gone, and Rigidbody is not yet active
        {
            ActivateRigidbody();
        }

        Invoke("DeleteCannonBall", 3f);
    }

    public void Setup(Vector3 enemyDirection, Monster incomingTargetedEnemy)
    {
        targetedEnemy = incomingTargetedEnemy; // who to chase?
        lastDirection = (targetedEnemy.getHitTarget().position - transform.position).normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
       if ((groundLayers & (1<< collision.gameObject.layer)) != 0)
        {
            onGround = true;
        }
    }

    IEnumerator Spin()
    {
        while (!onGround)
        {
            // Apply a random rotation around the y-axis
            float randomYRotation = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

            // Apply continuous angular velocity for spinning effect
            // rb.angularVelocity = Random.insideUnitSphere * rotationSpeed; // Adjust rotation speed as needed
           
            yield return new WaitForSeconds(waitTime);
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

    private void DeleteCannonBall() 
    {
        // sound effect
        // vfx
        base.OnDisable();
    }

    private void ActivateRigidbody()
    {
        rb.isKinematic = false; // Allow the Rigidbody to be affected by physics
        rb.velocity = lastDirection * cannonSpeed; // Continue in the last known direction
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (targetedEnemy != null && other.gameObject == targetedEnemy.gameObject)
        {
            targetedEnemy.TakeDamage(cannonDamage);
            this.gameObject.SetActive(false);

            // VFX
            GameObject newImpact = Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(newImpact, 1f);
        }
 
        // Get destroyed anyway
        DeleteCannonBall();
    }
}
