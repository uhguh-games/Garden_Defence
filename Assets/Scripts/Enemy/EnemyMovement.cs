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
    private GameObject cropToEat;
    private bool foundCrop;

    void Awake() 
    {
        target = GameObject.Find("MonsterTarget").GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true; // Ensure NavMeshAgent is enabled
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

    private IEnumerator FollowTarget() 
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while(enabled) 
        {
            if (agent.isOnNavMesh && !foundCrop ) // Check if agent is on NavMesh before setting destination
            {
                agent.SetDestination(target.position);
            }
            //note: i hate this implementation but i cant think of any other way
            if (agent.transform.position.x == target.position.x) //i set it to x because the y and z will always be different
            {
                //Debug.Log(agent.destination);
                foundCrop = true; //tell it not to set the destination to MonsterTarget anymore
                cropToEat = GameObject.FindWithTag("Crop"); //find a crop
                if (cropToEat == null) //i really hate this implementation but if all the crops have an enemy eating it it's going to break the script anyway since theres no more crops with the tag "Crop", and at that point you would have lost anyway. this will do for now
                {
                    break;
                }
                agent.SetDestination(cropToEat.transform.position); //go to crop
                cropToEat.tag = "CropEaten"; //change the selected crop's tag so that the other enemies don't choose it during FindWithTag
            }
          
            else
            {
                // Debug.LogWarning("Enemy is not on NavMesh.");
            }
            yield return wait;
        }

        // Once reach target (the crops): perform animation, play nomnom sound
        // Walk off screen and despawn.
    }
}
