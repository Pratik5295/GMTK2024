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
        //float offset = 0.5f;
        if(CanShoot(transform))
        {
            Debug.Log("can shoot should be true" + CanShoot(transform));
            if(ObjectPool.Instance != null)
            {
                GameObject enemyProjectile = ObjectPool.Instance.GetPooledEnemyProjectiles();

                if(enemyProjectile != null)
                {
                    enemyProjectile.transform.position = transform.position;
                    enemyProjectile.transform.rotation = Quaternion.identity;
                    enemyProjectile.SetActive(true);
                }
                ProjectileMover projectileMover = enemyProjectile.GetComponent<ProjectileMover>();
                projectileMover.Direction = transform.forward;
            } 
        }
    }
    private bool CanShoot(Transform transform)
    {
        ///checks if there aren't any players in front of the enemy who is going to shoot, that way it avoids enemies shooting at enemies
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 10f);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if(hit.collider.gameObject.tag == IStringDefinitions.ENEMY_TAG)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        return true;
    }
}
