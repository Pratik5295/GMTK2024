using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour, ISetupScriptableObject
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private float _walkRange;
    [SerializeField] private LayerMask _ground;

    private GameObject _player;
    private LineOfSight _lineOfSight;
    private Vector3 _positionToGoTo;
    private bool _playerInAttackRange;
    private bool _alreadyAttacked;
    private bool _walkPointSet;
    private float _instanceHealth;
    private float _instanceSpeed;
    private float _instanceDamageDealt;
    private float _instanceAttackDistance;
    private float _instanceTimeBetweenAttacks;


    public void SetupScriptableObject()
    {
        _instanceHealth = _enemySO._health;
        _instanceSpeed = _enemySO._speed;
        _instanceDamageDealt = _enemySO._damageDealt;
        _instanceAttackDistance = _enemySO._attackDistance;
        _instanceTimeBetweenAttacks = _enemySO._timeBetweenAttacks;
    }
    private void OnEnable()
    {
        _agent.stoppingDistance = _instanceAttackDistance;
        SetupScriptableObject();
    }
    private void Start()
    {
        _agent.stoppingDistance = _instanceAttackDistance;
        _player = GameObject.FindWithTag(IStringDefinitions.PLAYER_TAG);
        _lineOfSight = GetComponent<LineOfSight>();
        SetupScriptableObject();
    }
    private void Update()
    {
        if(!_playerInAttackRange && !IsPlayerWithinView()) Patrol();
        if(IsPlayerWithinView() && !_playerInAttackRange) Chase();
        if(IsPlayerWithinView() && _playerInAttackRange) Attack();
    }
    private void Patrol()
    {
        if (!_walkPointSet) SetWalkPosition();
        if (_walkPointSet) _agent.SetDestination(_positionToGoTo);

        Vector3 distanceToWalkPoint = transform.position - _positionToGoTo;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude <= 1f)
        {
            _walkPointSet = false;
            _positionToGoTo = Vector3.zero;
        }
    }
    private void SetWalkPosition()
    {
        float randomZ = UnityEngine.Random.Range(-_walkRange, _walkRange);
        float randomX = UnityEngine.Random.Range(-_walkRange, _walkRange);

        _positionToGoTo = new Vector3(transform.position.x  + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(_positionToGoTo, -transform.up, 2f, _ground))
        {
            _walkPointSet = true;
        }
    }
    private void Chase()
    {
        Debug.Log("chase being called");
        _agent.SetDestination(_player.transform.position);
        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _playerInAttackRange = true;
        } 
        else 
        {
            _playerInAttackRange = false;
        }

    }
    private void Attack()
    {
        //Make sure enemy doesn't move
        _agent.SetDestination(transform.position);

        transform.LookAt(_player.transform);

        if (!_alreadyAttacked)
        {
            Observer.Instance.EnemyAttack(_instanceDamageDealt, gameObject);
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _instanceTimeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
        _playerInAttackRange = false;
    }
    private GameObject IsPlayerWithinView()
    {
        if(_lineOfSight.ObjectsWithingSensor.Count > 0)
        {
            return _lineOfSight.ObjectsWithingSensor[0];
        }
        return null;
    }
}
