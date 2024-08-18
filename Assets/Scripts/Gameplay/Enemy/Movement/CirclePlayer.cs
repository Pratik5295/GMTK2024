using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "CirclePlayer", menuName = "ScriptableObjects/Enemy/Movement/CirclePlayer")]
public class CirclePlayer : MovementStrategy
{
    private Vector3 _directionToGo;
    public override void Move(NavMeshAgent agent, Transform player)
    {
        HandleCircleBehaviour(agent, player);
    }
    private void HandleCircleBehaviour(NavMeshAgent agent, Transform player)
    {
        if(agent.stoppingDistance >= agent.remainingDistance)
        {
            Vector3 directionToPlayer = agent.transform.position - player.transform.position;
            _directionToGo = Vector3.Cross(directionToPlayer, agent.transform.up);
            agent.SetDestination(_directionToGo);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float radius = 0.5f;
        Gizmos.DrawWireSphere(_directionToGo, radius);
    }
}
