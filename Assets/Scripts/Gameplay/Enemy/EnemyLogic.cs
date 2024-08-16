using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour, ISetupScriptableObject
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySO _enemySO;
    private GameObject _player;

    #region Instance variables
    private float _instanceHealth;
    private float _instanceSpeed;
    private float _chaseTimer = 0;
    private float _timeToChase = 1;
    #endregion

    #region Getters and Setters
    public EnemySO EnemySO {get{return _enemySO;}}
    public float InstaceHealth{get{return _instanceHealth;} set{_instanceHealth = value;}}
    public float InstanceSpeed{get{return _instanceSpeed;} set{_instanceSpeed = value;}}
    #endregion
    public void SetupScriptableObject()
    {
        _instanceHealth = _enemySO._health;
        _instanceSpeed = _enemySO._speed;
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
        _chaseTimer += Time.deltaTime;
        if(_player != null)
        {
            if(_chaseTimer >= _timeToChase)
            {
                Debug.Log("agent should chase player");
                _chaseTimer = 0;
                _agent.SetDestination(_player.transform.position);
                if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
                {
                    Attack();
                }
            }
        }
    }
    private void Attack()
    {
        Debug.Log("implement attack logic");
    }
}
