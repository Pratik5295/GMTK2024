using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RegularChase", menuName = "ScriptableObjects/Enemy/Movement/Regular")]
public class RegularChase : MovementStrategy
{
    public override void Move(NavMeshAgent agent, Transform player)
    {
        RegularMovement(agent, player);
    }
    private void RegularMovement(NavMeshAgent agent, Transform player)
    {
        agent.SetDestination(player.transform.position);
        Debug.Log("regular chase being called");
    }
}
