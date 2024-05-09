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
    [SerializeField] public GameObject cropToEat;
    private bool foundCrop;
    [SerializeField] HealthManager healthManager;
    CapsuleCollider enemyCollider;
    Enemy enemyScript;

    void Awake() 
    {
        target = GameObject.Find("MonsterTarget").GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true; // Ensure NavMeshAgent is enabled
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        enemyCollider = GetComponent<CapsuleCollider>();
        enemyScript = GetComponent<Enemy>();

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
    
    public void OnEnemyDeath() 
    {
        healthManager.AddCrop(cropToEat);
    }

    public void WalkOffScreen()
    {
        agent.SetDestination(target.transform.position);
    }

    public void IgnoreCrop() 
    {
        enemyScript.enemyHungry = false;
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
                        IgnoreCrop();
                        WalkOffScreen();
                    }

                    if (cropToEat == null && healthManager.cropList.Count == 0)
                    {
                        IgnoreCrop();
                        WalkOffScreen();
                        //break;
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
