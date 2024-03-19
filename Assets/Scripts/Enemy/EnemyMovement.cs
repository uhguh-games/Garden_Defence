using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public Transform target;
    private float updateSpeed = 0.1f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Coroutine FollowCoroutine;

    void Awake() 
    {
        target = GameObject.Find("MonsterTarget").GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true; // Ensure NavMeshAgent is enabled
    }

    public void StartChasing()
    {
        if (FollowCoroutine == null) 
        {
            FollowCoroutine = StartCoroutine(FollowTarget());
        } 
        else 
        {
            Debug.LogWarning("Called StartChasing on Enemy that is already chasing.");
        }
    }

    private IEnumerator FollowTarget() 
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while(enabled) 
        {
            if (agent.isOnNavMesh) // Check if agent is on NavMesh before setting destination
            {
                agent.SetDestination(target.position);
            }
            else
            {
                Debug.LogWarning("Enemy is not on NavMesh.");
            }
            yield return wait;
        }

        // Once reach target (the crops): perform animation, play nomnom sound
        // Walk off screen and despawn.
    }
}
