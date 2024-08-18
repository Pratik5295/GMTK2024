using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour, ISetupScriptableObject
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private MovementStrategy _movementStrategy;
    [SerializeField] private AttackStategy _attackStrategy;
    [SerializeField] private LayerMask _playerLayer;
    private GameObject _player;


    private bool _playerInAttackRange;
    private bool _alreadyAttacked;
    private EnemySO _instanceEnemySO;
    private MovementStrategy _instanceMovementStrategy;
    private AttackStategy _instanceAttackStrategy;
    private float _instanceHealth;
    private float _instanceSpeed;
    private float _instanceDamageDealt;
    private float _instanceAttackDistance;
    private float _instanceTimeBetweenAttacks;


    public void SetupScriptableObject()
    {
        _instanceEnemySO = Instantiate(_enemySO);
        _instanceMovementStrategy = Instantiate(_movementStrategy);
        _instanceAttackStrategy = Instantiate(_attackStrategy);

        _instanceHealth = _instanceEnemySO._health;
        _instanceSpeed = _instanceEnemySO._speed;
        _instanceDamageDealt = _instanceEnemySO._damageDealt;
        _instanceAttackDistance = _instanceAttackStrategy._attackDistance;
        _instanceTimeBetweenAttacks = _instanceAttackStrategy._timeBetweenAttacks;
    }
    private void OnEnable()
    {
        SetupScriptableObject();
    }
    private void Start()
    {
        _player = GameObject.FindWithTag(IStringDefinitions.PLAYER_TAG);
        SetupScriptableObject();
    }
    private void Update()
    {
        _playerInAttackRange = Physics.CheckSphere(transform.position, _instanceAttackDistance, _playerLayer);

        if(!_playerInAttackRange) Chase();
        if(_playerInAttackRange) Attack();

        Debug.Log("already attacked" + _alreadyAttacked);
    }
    private void Chase()
    {
        _movementStrategy.Move(_agent, _player.transform);
    }
    private void Attack()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_player.transform);

        if (!_alreadyAttacked)
        {
            _attackStrategy.Attack(transform, this);
            StartCoroutine(ResetAttack(_instanceTimeBetweenAttacks));
        }
    }
    private IEnumerator ResetAttack(float attackCooldDown)
    {
        _alreadyAttacked = true;
        Debug.Log("reset attack should be called");
        yield return new WaitForSeconds(attackCooldDown);
        _alreadyAttacked = false;
    }
}
