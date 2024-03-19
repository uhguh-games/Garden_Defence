using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : PoolableObject
{
    public EnemyMovement Movement;
    public NavMeshAgent Agent;

    public override void OnDisable()
    {
        base.OnDisable();

        if (Agent != null) 
        {
            Agent.enabled = false;
        }
    }
}
