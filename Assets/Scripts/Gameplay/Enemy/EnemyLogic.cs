using Unity.Mathematics;

using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private AttackStategy _attackStrategy;

    [SerializeField] private float _damageDealt = 1;
    [SerializeField] private float _attackCooldown = 10;
    [SerializeField] private float _chargeUp = 1;

    private Vector3 _originalSize;
    private float _chaseTimer = 0;
    private float _timeToChase = 0.25f;
    private bool _hasAttacked = false;
    private float _originalSpeed;
    public EnemyType EnemyType {get{return _enemyType;}}
    public float DamageDealt {get{return _damageDealt;} set{_damageDealt = value;}}
    private void OnEnable()
    {
        _originalSpeed = _agent.speed;
        _chaseTimer = 0;
        _hasAttacked = false;
        _attackStrategy = Instantiate(_attackStrategy);
        _originalSize = transform.localScale;
        transform.localScale = _originalSize;
        _agent.speed = _originalSpeed;
    }
    private void Start()
    {
        _chaseTimer = 0;
        _hasAttacked = false;
        _originalSpeed = _agent.speed;
        _attackStrategy = Instantiate(_attackStrategy);
        _originalSize = transform.localScale;
        transform.localScale = _originalSize;
        _agent.speed = _originalSpeed;
    }
    private void Update()
    {
        _chaseTimer += Time.deltaTime;
        transform.LookAt(PlayerMovement.player.transform);
        if(PlayerMovement.player != null)
        {
            if(_chaseTimer >= _timeToChase)
            {
                _chaseTimer = 0;
                if(_agent.enabled)
                {
                    _agent.SetDestination(PlayerMovement.player.transform.position);
                    if(_hasAttacked == false) Observer.Instance.EnemyChase(gameObject);
                }
                if(AttackConditionsWereMet())
                {
                    Observer.Instance.WaitToAttack(gameObject);
                    Invoke("WaitToAttack", _chargeUp);
                }
            }
        }
    }
    private bool AttackConditionsWereMet() => _agent.enabled && _agent.isOnNavMesh && _agent.remainingDistance <= _agent.stoppingDistance && !_hasAttacked;
    private void WaitToAttack()
    {
        _agent.speed = 0;
        Attack();
    }
    private void Attack()
    {
        _capsuleCollider.excludeLayers = LayerMask.GetMask("Player");
        Transform currentPlayerPosition = PlayerMovement.player.transform;
        _agent.speed = 0;
        _hasAttacked = true;
        Observer.Instance.EnemyAttack(gameObject);
        _attackStrategy.Attack(new AttackStategyParamethers(_agent, currentPlayerPosition, _damageDealt, this));
        if(gameObject.activeSelf) Invoke("ResetAttack", _attackCooldown);
        
    }
    private void ResetAttack()
    {
        _capsuleCollider.excludeLayers = _capsuleCollider.includeLayers;
        _agent.speed = _originalSpeed;
        _hasAttacked = false;
    }
}
