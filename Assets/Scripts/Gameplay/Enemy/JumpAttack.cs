using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "ScriptableObjects/Enemy/Attacks/JumpAttack")]
public class JumpAttack : AttackStategy
{
    public LayerMask playerLayer;
    public float _jumpSpeed;
    public AnimationCurve _heightCurve;
    private Vector3 trackedPlayerPosition;
    public float attackRadius = 0.3f;
    public override void Attack(AttackStategyParamethers e)
    {
        if(e.Agent.isActiveAndEnabled) e.MonoBehaviour.StartCoroutine(JumpLogic(e.Agent, e.PlayerTransform, e.DamageDealt));
    }
    private IEnumerator JumpLogic(NavMeshAgent agent, Transform playerTransform, float damageDealt)
    {
        //Debug.Log("jump being called");
        agent.enabled = false;
        Vector3 startingPosiiton = agent.transform.position;
        RaycastHit hit;


        bool playerPositionWasTaken = false;
        if(!playerPositionWasTaken) trackedPlayerPosition = playerTransform.position;

        for(float time = 0; time < 1; time += Time.deltaTime * _jumpSpeed)
        {
            //Make the enemie position lerp towards the player
            agent.transform.position = Vector3.Lerp(startingPosiiton, trackedPlayerPosition, time)
            + Vector3.up * _heightCurve.Evaluate(time);

            //Make sure the enemie is facing the player
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation,
            Quaternion.LookRotation(trackedPlayerPosition - agent.transform.position), time);

            //Detect collision with player
            if(Physics.SphereCast(agent.transform.position, attackRadius, agent.transform.forward, 
            out hit, agent.stoppingDistance, playerLayer))
            {
                if(hit.collider.CompareTag(IStringDefinitions.PLAYER_TAG))
                {
                    //Debug.Log("Jump attack hit player");
                    PlayerHealth playerHealth = hit.collider.GetComponentInParent<PlayerHealth>();
                    playerHealth.ReduceHealth(damageDealt);
                    yield return null;
                } 
            }
            yield return null;
        }

        //Reenable agent since we manually moved it and make sure it understands where it is now
        agent.enabled = true;
        if(NavMesh.SamplePosition(trackedPlayerPosition, out NavMeshHit navMeshHit, 3f, agent.areaMask) && agent.isOnNavMesh)
        {
            agent.Warp(navMeshHit.position);
        }
    }
}
