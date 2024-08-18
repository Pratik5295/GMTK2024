using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour, ISetupScriptableObject
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySO _enemySO;
    private GameObject _player;
    private EnemySO _instanceEnemySO;
    private float _chaseTimer;
    private float _timeToChase = 0.25f;

    public void SetupScriptableObject()
    {
        _instanceEnemySO = Instantiate(_enemySO);
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
                _chaseTimer = 0;

                _agent.SetDestination(PlayerMovement.player.transform.position);
            }
        }
    }
}
