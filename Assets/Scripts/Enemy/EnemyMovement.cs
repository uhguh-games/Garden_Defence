using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float updateSpeed = 0.1f;
    private UnityEngine.AI.NavMeshAgent agent;

    void Awake() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget() 
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while(enabled) 
        {
            agent.SetDestination(target.transform.position);
            transform.LookAt(target);
            yield return wait;
        }

        // Once reach target (the crops): perform animation, play nomnom sound
        // Walk off screen despawn.
    }


    
}
