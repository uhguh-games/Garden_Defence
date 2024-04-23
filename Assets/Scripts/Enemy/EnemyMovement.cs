using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public Transform target;
    private float updateSpeed = 0.1f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Coroutine FollowCoroutine;
    [SerializeField] private GameObject cropToEat;
    private bool foundCrop;

    [SerializeField] HealthManager healthManager;

    void Awake() 
    {
        target = GameObject.Find("MonsterTarget").GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true; // Ensure NavMeshAgent is enabled

        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();

        foundCrop = false;
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

    public void WalkOffScreen()
    {
        agent.SetDestination(target.transform.position);
    }

    private IEnumerator FollowTarget() 
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while(enabled) 
        {
            if (agent.isOnNavMesh) // Check if agent is on NavMesh before setting destination
            {
                if (healthManager.cropList.Count > 0 && !foundCrop)
                {
                    foundCrop = true; //tell it not to set the destination to MonsterTarget anymore

                    cropToEat = healthManager.GetRandomCrop();

                    if (cropToEat != null) 
                    {
                        healthManager.RemoveCrop(cropToEat);
                        agent.SetDestination(cropToEat.transform.position); //go to crop
                    } 
                    else 
                    {

                        Debug.LogWarning("No crops available");
                        WalkOffScreen();
                    }

                    if (healthManager.cropList.Count == 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Enemy is not on NavMesh.");
            }

            yield return wait;
        }
    }
}
