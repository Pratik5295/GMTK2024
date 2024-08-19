using UnityEngine;


[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Enemy/Attacks/Projectile")]
public class ProjectileAttack : AttackStategy
{
    public override void Attack(AttackStategyParamethers e)
    {
        RangedAttack(e.BulletSpawner, e.DamageDealt);
    }
    private void RangedAttack(Transform bulletSpawner, float damageDealt)
    {
        Debug.Log("Ranged attack being called");
        Vector3 directionToShoot = PlayerMovement.player.transform.position - bulletSpawner.transform.position ;
        Debug.DrawRay(bulletSpawner.transform.position, directionToShoot, Color.red, 10);
        if(!CheckForObstacles(bulletSpawner, directionToShoot)) SpawnBullet(bulletSpawner, damageDealt, directionToShoot);
    }
    private bool CheckForObstacles(Transform bulletSpawner, Vector3 directionToShoot)
    {
        RaycastHit hit;
        //checks if there is something in front of enemy, don't shoot
        if(Physics.Raycast(bulletSpawner.transform.position, directionToShoot, out hit, 5))
        {
            if(hit.collider.gameObject.tag == IStringDefinitions.PLAYER_TAG)
            {
                return true;
            } 
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
    private void SpawnBullet(Transform bulletSpawner, float damageDealt, Vector3 directionToShoot)
    {
        GameObject projectile = ObjectPool.Instance.GetPooledEnemyProjectiles();
        if(projectile != null)
        {

            projectile.transform.position = bulletSpawner.transform.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        }
        ProjectileMover projectileMover = projectile.GetComponent<ProjectileMover>();
        projectileMover.Direction = directionToShoot;
        projectileMover.DamageDealt = damageDealt;
    }
}
