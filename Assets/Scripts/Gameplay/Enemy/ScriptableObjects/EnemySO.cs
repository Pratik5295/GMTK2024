using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    public EnemyType _enemyType;
    public float _health;
    public float _speed;
}
