using UnityEngine;

public class ArmCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider _rightArmCollider;
    [SerializeField] private BoxCollider _leftArmCollider;
    public void SetMeeleeAttack()
    {
        Debug.Log("set meele attack being called");
        _rightArmCollider.isTrigger = false;
        _leftArmCollider.isTrigger = false;
    }
    public void ResetMeeleeAttack()
    {
        Debug.Log("reset meelee attack being called");
        _leftArmCollider.isTrigger = true;
        _rightArmCollider.isTrigger = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision with something");
        if(other.gameObject.CompareTag(IStringDefinitions.PLAYER_TAG))
        {
            Debug.Log("collision with plauyer");
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.ReduceHealth(1);
        }
    }
}
