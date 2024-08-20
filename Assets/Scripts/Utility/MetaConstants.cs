using System.Collections.Generic;
using UnityEngine;
using static MetaConstants;

/// <summary>
/// A class to hold all the constants values in game
/// </summary>
public static class MetaConstants
{

    #region Player Input Constants

    public const KeyCode readDialog = KeyCode.X;

    #endregion

    #region Player Health System

    public const float MaxPlayerHealth = 100f;

    public const string MatShaderOverlay = "_OverlayAmount";

    #endregion

    public enum DeathCodes
    {
        NORMAL = 0,
        STOMPED = 1,
        NOOB = 2,
        DESPERATE = 3
    }
}

public static class DeathMessages
{
    public static readonly Dictionary<DeathCodes, string> Messages = new Dictionary<DeathCodes, string>
    {
        { DeathCodes.NORMAL, "You died" },
        { DeathCodes.STOMPED , "How does it feel to be stomped to death!?" },
        { DeathCodes.NOOB, "You are a big noob aren't you?" },
        { DeathCodes.DESPERATE, "You dont love me anymore, do you?"}
    };

    public static string GetDeathMessage(DeathCodes code)
    {
        return Messages.TryGetValue(code, out string message) ? message : "Unknown error.";
    }
}

