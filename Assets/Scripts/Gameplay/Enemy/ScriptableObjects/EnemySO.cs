using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    public EnemyType _enemyType;
    public float _health;
    public float _speed;
    public float _damageDealt;
    public float _timeBetweenAttacks;
}
