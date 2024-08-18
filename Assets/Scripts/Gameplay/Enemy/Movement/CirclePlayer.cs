using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "CirclePlayer", menuName = "ScriptableObjects/Enemy/Movement/CirclePlayer")]
public class CirclePlayer : MovementStrategy
{
    public override void Move(NavMeshAgent agent, Transform player)
    {
        HandleCircleBehaviour(agent, player);
    }
    private void HandleCircleBehaviour(NavMeshAgent agent, Transform player)
    {
        if(agent.stoppingDistance <= agent.remainingDistance)
        {
            Vector3 directionToPlayer = agent.transform.position - player.transform.position;
            Vector3 directionToGo = Vector3.Cross(directionToPlayer, agent.transform.up);
        }
    }
}
