using UnityEngine;
using UnityEngine.AI;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract void Move(NavMeshAgent agent, Transform player);
}
