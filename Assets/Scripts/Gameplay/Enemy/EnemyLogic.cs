using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour, ISetupScriptableObject
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private MovementStrategy _movementStrategy;
    [SerializeField] private AttackStategy _attackStrategy;
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

    private RaycastHit hit;
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
        if(Physics.SphereCast(transform.position, _instanceAttackDistance, transform.forward, out hit))
        {
            Debug.Log("physics check sphere");
            if(hit.collider.gameObject.tag ==IStringDefinitions.PLAYER_TAG)
            {
                Debug.Log("object collided with is player");
                _playerInAttackRange = true;
            } else
            {
                Debug.Log("is not the player:" + hit.collider.gameObject.name);
            }
        }

        if(!_playerInAttackRange) Chase();
        if(_playerInAttackRange) Attack();

        transform.LookAt(_player.transform);
        Debug.Log("already attacked" + _alreadyAttacked);
        Debug.Log("player in attack range: " + _playerInAttackRange);
    }
    private void Chase()
    {
        _movementStrategy.Move(_agent, _player.transform);
        Observer.Instance.EnemyChase(gameObject);
    }
    private void Attack()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_player.transform);

        if (!_alreadyAttacked)
        {
            Debug.Log("enemy should attack");
            _attackStrategy.Attack(transform, _player.transform, this);
            Observer.Instance.EnemyAttack(gameObject);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _instanceAttackDistance);
        
    }
}
