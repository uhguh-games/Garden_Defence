using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] float damage = 5f;
    [SerializeField] GameObject impact;
    [SerializeField] float waitTime = 0.1f;
    [SerializeField] LayerMask groundLayers;

    private Rigidbody rb;
    private Monster targetedEnemy;
    private Vector3 lastDirection;
    public bool onGround = false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Prevent the Rigidbody from affecting the projectile initially
        StartCoroutine(Spin());
    }

    public void Setup(Vector3 enemyDirection, Enemy incomingTargetedEnemy)
    {
        targetedEnemy = incomingTargetedEnemy; // who to chase?
        lastDirection = (targetedEnemy.getHitTarget().position - transform.position).normalized;
    }
    private void DestroyStrayBullet()
    {
        Destroy(this.gameObject);
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
            rb.angularVelocity = Random.insideUnitSphere * rotationSpeed; // Adjust rotation speed as needed
           
            yield return new WaitForSeconds(waitTime);
        }
 
    }

    private void Update()
    {
        
        if (targetedEnemy) // if the targeted enemy is still alive
        {
            lastDirection = (targetedEnemy.getHitTarget().position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetedEnemy.getHitTarget().position,
                    speed * Time.deltaTime);
        }
        else if (rb.isKinematic) // Target is gone, and Rigidbody is not yet active
        {
            ActivateRigidbody();
        }
        Invoke("DestroyStrayBullet", 3f);
    }



    private void ActivateRigidbody()
    {
        rb.isKinematic = false; // Allow the Rigidbody to be affected by physics
        rb.velocity = lastDirection * speed; // Continue in the last known direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetedEnemy != null && other.gameObject == targetedEnemy.gameObject)
        {
            targetedEnemy.InflictDamage(damage);
            GameObject newImpact = Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(newImpact, 1f);
        }
             Destroy(this.gameObject);
      
    }

    */

}