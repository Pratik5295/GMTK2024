using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Enemy/Attacks/Projectile")]
public class ProjectileAttack : AttackStategy
{
    public override void Attack(AttackStategyParamethers e)
    {
        Debug.Log("attack being called");
        ShootAtPlayer(e.BulletSpawner, e.Player.transform);
    }


    public void ShootAtPlayer(Transform bulletSpawner, Transform player)
    {
        Debug.Log("shoot being called");
        //float offset = 0.5f;
        if(CanShoot(bulletSpawner))
        {
            Debug.Log("can shoot should be true" + CanShoot(bulletSpawner));
            if(ObjectPool.Instance != null)
            {
                GameObject enemyProjectile = ObjectPool.Instance.GetPooledEnemyProjectiles();

                if(enemyProjectile != null)
                {
                    enemyProjectile.transform.position = bulletSpawner.position;
                    enemyProjectile.transform.rotation = Quaternion.identity;
                    enemyProjectile.SetActive(true);
                }
                ProjectileMover projectileMover = enemyProjectile.GetComponent<ProjectileMover>();
                Vector3 directionToShoot = bulletSpawner.position - player.transform.position;
                projectileMover.Direction = -directionToShoot;
            } 
        }
        {
            Debug.Log("can shoot false");
        }
    }
    private bool CanShoot(Transform transform)
    {
        Debug.Log("can shoot being called");
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
