using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float damageCaused;
    [SerializeField] private Rigidbody _rb;
    private float _damage;
    private Vector3 _direction;
    private float timer;
    private float timeToDestroy = 5;
    public Vector3 Direction{get{return _direction;} set{_direction = value;}}
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        MoveProjectile(_speed);
        if(timer >= timeToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
    public void MoveProjectile(float speed)
    {
        _rb.AddForce(_direction * speed);
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(IStringDefinitions.PLAYER_TAG))
        {
            Vector3 collisionNormal = other.contacts[0].normal;
            gameObject.SetActive(false);

            PlayerHealth playerHealth = other.collider.transform.parent.GetComponent<PlayerHealth>();
            playerHealth.ReduceHealth(damageCaused);
        }
    }
}
