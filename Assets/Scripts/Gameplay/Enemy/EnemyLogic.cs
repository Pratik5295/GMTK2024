using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _attackRange = 1;
    [SerializeField] private float _damageDealt = 1;
    private Vector3 _originalSize;
    private Entity _entity;
    private float _originalHealth;
    private float _chaseTimer = 0;
    private float _timeToChase = 0.25f;
    private float _attackCooldown = 2;
    private bool _hasAttacked = false;
    public EnemyType EnemyType {get{return _enemyType;}}
    private void OnEnable()
    {
        _originalHealth = _entity.Health;
        _agent.stoppingDistance = _attackRange;
        _originalSize = transform.localScale;
        _agent.speed = _speed;
        transform.localScale = _originalSize;
        _entity.Health = _originalHealth;
    }
    private void Start()
    {
        _originalHealth = _entity.Health;
        _agent.stoppingDistance = _attackRange;
        _originalSize = transform.localScale;
        _agent.speed = _speed;
        transform.localScale = _originalSize;
        _entity.Health = _originalHealth;
    }
    private void Update()
    {
        _chaseTimer += Time.deltaTime;
        if(PlayerMovement.player != null)
        {
            if(_chaseTimer >= _timeToChase)
            {
                _chaseTimer = 0;
                _agent.SetDestination(PlayerMovement.player.transform.position);
                if(_agent.remainingDistance <= _agent.stoppingDistance && !_hasAttacked)
                {
                    Debug.Log("attack should be called");
                    //Attack();
                }
            }
        }
    }
    private void Attack()
    {
        Debug.Log("attack being called");
        _hasAttacked = true;
        switch(_enemyType)
        {
            case EnemyType.Basic:
                MeeleeAttack();
                break;
            case EnemyType.Fast:
                RangedAttack();
                break;
            case EnemyType.Big:
                RangedAttack();
                break;
            default:
                break;
        }
        Invoke("ResetAttack", _attackCooldown);
    }
    private void MeeleeAttack()
    {
        Debug.Log("meelee attack being called");
        //_meeleeAttackCollider.isTrigger = false;
    }
    private void RangedAttack()
    {
        Debug.Log("ranged attack being called");
        Vector3 directionToShoot = _agent.transform.position - PlayerMovement.player.transform.position;
        GameObject projectile = ObjectPool.Instance.GetPooledEnemyProjectiles();
        if(projectile != null)
        {
            Debug.Log("projectile not null");
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        } else
        {
            Debug.Log("projectile was null");
        }
        ProjectileMover projectileMover = projectile.GetComponent<ProjectileMover>();
        projectileMover.Direction = -directionToShoot;
    }
    private void ResetAttack(float attackCooldown)
    {
        Debug.Log("reset attack being called");
        _hasAttacked = false;
    }
}
