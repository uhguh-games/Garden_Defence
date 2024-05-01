using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StoneProjectile : AutoDestroyPoolableObject
{
    [SerializeField] float projectileSpeed = 5f;
    // float rotationSpeed = 360f;
    [SerializeField] float projectileDamage = 1f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float waitTime = 0.1f;
    private Rigidbody rb;
    private Enemy targetedEnemy;
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
                    projectileSpeed * Time.deltaTime);
        }
        else if (rb.isKinematic) // Target is gone, and Rigidbody is not yet active
        {
            ActivateRigidbody();
        }

        Invoke("DeleteProjectile", 3f);
    }

    public void Setup(Vector3 enemyDirection, Enemy incomingTargetedEnemy)
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
            // rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
           
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

    private void DeleteProjectile() 
    {
        // base.OnDisable();
        this.gameObject.SetActive(false);
    }

    private void ActivateRigidbody()
    {
        rb.isKinematic = false; // Allow the Rigidbody to be affected by physics
        rb.velocity = lastDirection * projectileSpeed; // Continue in the last known direction
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (targetedEnemy != null && other.gameObject == targetedEnemy.gameObject)
        {
            targetedEnemy.TakeDamage(projectileDamage);
            this.gameObject.SetActive(false);

            // VFX
            GameObject newImpact = Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(newImpact, 1f);
        }
 
        // Get destroyed anyway
        DeleteProjectile();
    }
}
