using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "ScriptableObjects/Enemy/Attacks/JumpAttack")]
public class JumpAttack : AttackStategy
{
    public LayerMask playerLayer;
    public float _jumpSpeed;
    public AnimationCurve _heightCurve;
    public override void Attack(AttackStategyParamethers e)
    {
        e.MonoBehaviour.StartCoroutine(JumpLogic(e.Agent, e.PlayerTransform, e.DamageDealt));
    }
    private IEnumerator JumpLogic(NavMeshAgent agent, Transform playerTransform, float damageDealt)
    {
        //Debug.Log("jump being called");
        agent.enabled = false;
        Vector3 startingPosiiton = agent.transform.position;
        RaycastHit hit;

        for(float time = 0; time < 1; time += Time.deltaTime * _jumpSpeed)
        {
            //Make the enemie position lerp towards the player
            agent.transform.position = Vector3.Lerp(startingPosiiton,playerTransform.position, time)
            + Vector3.up * _heightCurve.Evaluate(time);

            //Make sure the enemie is facing the player
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation,
            Quaternion.LookRotation(playerTransform.position - agent.transform.position), time);

            //Detect collision with player
            float attackRadius = 0.5f;
            if(Physics.SphereCast(agent.transform.position, attackRadius, agent.transform.forward, 
            out hit, agent.stoppingDistance, playerLayer))
            {
                if(hit.collider.CompareTag(IStringDefinitions.PLAYER_TAG))
                {
                    //Debug.Log("Jump attack hit player");
                    PlayerHealth playerHealth = hit.collider.GetComponentInParent<PlayerHealth>();
                    playerHealth.ReduceHealth(damageDealt);
                }
            }

            yield return null;
        }

        //Reenable agent since we manually moved it and make sure it understands where it is now
        agent.enabled = true;
        if(NavMesh.SamplePosition(playerTransform.position, out NavMeshHit navMeshHit, 1f, agent.areaMask))
        {
            agent.Warp(navMeshHit.position);
        }
    }
}
