using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "DodgeChase", menuName = "ScriptableObjects/Enemy/Movement/DodgeChase")]
public class DodgeChase : MovementStrategy
{
    public float _dodgeRange;
    public float _dodgeFrequency;
    public float _dodgeSpeed;

    private bool _canDodge = false;
    private float _timer;
    private float _originalSpeed;
    private Vector3 _directionToDodgeTo;

    public override void Move(NavMeshAgent agent, Transform player)
    {
        _originalSpeed = agent.speed;
        if(!_canDodge) agent.SetDestination(player.transform.position);
        _timer += Time.deltaTime;
        if(_timer >= _dodgeFrequency)
        {
            _canDodge = true;
        }
        DodgeMovement(agent, player);

        Vector3 distanceToWalkPoint = agent.transform.position - _directionToDodgeTo;
        if(distanceToWalkPoint.magnitude < 1)
        {
            Debug.Log("agent reached dodge target, should go back to chasing player");
            _canDodge = false;
            _timer = 0;
            agent.speed = _originalSpeed;
        }
    }
    private void DodgeMovement(NavMeshAgent agent, Transform player)
    {
        if(_canDodge && agent.remainingDistance <= agent.stoppingDistance)
        {
            float randomZ = Random.Range(-_dodgeRange, _dodgeRange);
            float randomX = Random.Range(-_dodgeRange, _dodgeRange);

            _directionToDodgeTo = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);
            agent.SetDestination(_directionToDodgeTo);
            agent.speed = _dodgeSpeed;
        } else
        {
            _canDodge = false;
            _timer = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float radius = 3f;
        Gizmos.DrawWireSphere(_directionToDodgeTo, radius);
    }
}
