using UnityEngine.AI;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private AttackStategy _attackStrategy;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damageDealt = 1;
    [SerializeField] private float _attackCooldown = 10;
    [SerializeField] private float _attackRange;
    private bool _playerInAttackRange = false;
    private RaycastHit hit;
    private Vector3 _originalSize;
    private float _chaseTimer = 0;
    private float _timeToChase = 0.25f;
    private bool _hasAttacked = false;
    private float _originalSpeed;
    public EnemyType EnemyType {get{return _enemyType;}}
    private void OnEnable()
    {
        _originalSpeed = _agent.speed;
        _attackStrategy = Instantiate(_attackStrategy);
        _originalSize = transform.localScale;
        transform.localScale = _originalSize;
    }
    private void Start()
    {
        _originalSpeed = _agent.speed;
        _attackStrategy = Instantiate(_attackStrategy);
        _originalSize = transform.localScale;
        transform.localScale = _originalSize;
    }
    private void Update()
    {
        _playerInAttackRange = Physics.SphereCast(transform.position, _attackRange, transform.forward, out hit, _playerLayer);
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
                if(_agent.enabled && _playerInAttackRange && !_hasAttacked)
                {
                    Attack();
                }
            }
        }
    }
    private void Attack()
    {
        _agent.speed = 0;
        _hasAttacked = true;
        Observer.Instance.EnemyAttack(gameObject);
        _attackStrategy.Attack(new AttackStategyParamethers(_agent, gameObject.transform, _damageDealt, this));
        Invoke("ResetAttack", _attackCooldown);
        
    }
    private void ResetAttack()
    {
        _agent.speed = _originalSpeed;
        _hasAttacked = false;
        Observer.Instance.EnemyAttackEnded(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 0.5f);
    }
}
