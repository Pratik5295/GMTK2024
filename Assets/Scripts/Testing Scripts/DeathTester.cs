using UnityEngine;

/// <summary>
/// The purpose of this script is to provide an easier UI based way
/// to test out player death
/// </summary>
public class DeathTester : MonoBehaviour
{
    public void PlayerDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
