using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_AI : PoolableObject
{
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public EnemyScriptableObject EnemyScriptableObject;
    public int Health = 5;
    EnemyTracker enemyTracker;

    void Awake() 
    {
        enemyTracker = GameObject.Find("EnemyTracker").GetComponent<EnemyTracker>();
    }

    public virtual void OnEnable() 
    {
        SetUpAgentFromConfiguration();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (Agent != null) 
        {
            Agent.enabled = false;
        }

        // enemyTracker.RemoveEnemy(this);
    }

    public virtual void SetUpAgentFromConfiguration() 
    {
        Agent.acceleration = EnemyScriptableObject.Acceleration;
        Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
        Agent.areaMask = EnemyScriptableObject.AreaMask;
        Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
        Agent.baseOffset = EnemyScriptableObject.BaseOffset;
        Agent.height = EnemyScriptableObject.Height;
        Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
        Agent.radius = EnemyScriptableObject.Radius;
        Agent.speed = EnemyScriptableObject.Speed;
        Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;
        
        // Movement.UpdateRate = EnemyScriptableObject.AIUpdateInterval;

        Health = EnemyScriptableObject.Health;
    }
}
