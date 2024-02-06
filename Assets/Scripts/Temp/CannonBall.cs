using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : AutoDestroyPoolableObject
{
    [SerializeField] float cannonSpeed = 5f;
    [SerializeField] float cannonDamage = 1f;
    [SerializeField] private Rigidbody rb;
    private Monster targetedEnemy;
    private Vector3 lastDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null) 
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
    }

    public void Setup(Vector3 enemyDirection, Monster incomingTargetedEnemy)
    {
        targetedEnemy = incomingTargetedEnemy; // who to chase?
        lastDirection = (targetedEnemy.getHitTarget().position - transform.position).normalized;
    }
    private void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * cannonSpeed); old movement for testing

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
        }
 
        // Get destroyed anyway
        DeleteCannonBall();
    }
    
    #region Old OnTriggerEnter
    /*
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
    */
    #endregion
}
