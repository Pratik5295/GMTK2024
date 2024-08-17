using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Enemy/Attacks/Projectile")]
public class ProjectileAttack : AttackStategy
{
    public override void Attack(Transform transform, MonoBehaviour monoBehaviour)
    {
        ShootAtPlayer(transform);
    }
    public void ShootAtPlayer(Transform transform)
    {
        float offset = 0.5f;
        if(ObjectPool.Instance != null)
        {
            GameObject enemyProjectile = ObjectPool.Instance.GetPooledEnemyProjectiles();

            if(enemyProjectile != null)
            {
                enemyProjectile.transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                enemyProjectile.transform.rotation = Quaternion.identity;
                enemyProjectile.SetActive(true);
            }
            ProjectileMover projectileMover = enemyProjectile.GetComponent<ProjectileMover>();
            projectileMover.Direction = transform.forward;
        } 
    }
}
