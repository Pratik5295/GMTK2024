using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "DodgeChase", menuName = "ScriptableObjects/Enemy/Movement/DodgeChase")]
public class DodgeChase : MovementStrategy
{
    public float _dodgeRange;
    public float _dodgeCooldown;

    private float _timer = 0;
    private float _dashDuration;
    private bool _canDodge;
    private Vector3 _directionToDodgeTo;
    public override void Move(NavMeshAgent agent, Transform player)
    {
        DodgeMovement(agent, player);
    }
    private void DodgeMovement(NavMeshAgent agent, Transform player)
    {
        Debug.Log("dodge movement being called");
        if(!_canDodge) agent.SetDestination(player.transform.position);
        _timer += Time.deltaTime;
        if(_timer >= _dodgeCooldown)
        {
            Dodge(agent);
            if(_canDodge)
            {
                agent.SetDestination(_directionToDodgeTo);
                WaitForDashToEnd();
            }
        }
    }
    private void WaitForDashToEnd()
    {
        float dashTimer = 0;
        dashTimer += Time.deltaTime;
        if(dashTimer >= _dashDuration)
        {
            _canDodge = false;
            _timer = 0;
        }
    }
    private void Dodge(NavMeshAgent agent)
    {
        Debug.Log("DODGE being called");
        float randomX = Random.Range(-_dodgeRange, _dodgeRange);
        float randomZ = Random.Range(-_dodgeRange, _dodgeRange);

        _directionToDodgeTo = new Vector3(randomX, 0, randomZ);

        RaycastHit hit;
        Debug.DrawRay(agent.transform.position, _directionToDodgeTo, Color.red, 10);
        if(Physics.Raycast(agent.transform.position, _directionToDodgeTo, out hit))
        {
            if(hit.collider.gameObject == null)
            {
                Debug.Log("hit didn't found anything" + hit.collider.gameObject);
                _canDodge = true;
            } 
            else
            {
                Debug.Log("hit found something");
                _canDodge = false;
            
            }
        } else
        {
            Debug.Log("raycast didn't hit anything");
            _canDodge = true;
        }
    }
}
