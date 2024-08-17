using System;
using UnityEngine;
using static MetaConstants;

/// <summary>
/// The purpose of this script is to provide an easier UI based way
/// to test out player death
/// </summary>
public class DeathTester : MonoBehaviour
{
    [SerializeField] private DeathCodes DeathCode;
    public void PlayerDeath()
    {
        DeathScreen.Instance.SetDeathCode(DeathCode);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }

}
