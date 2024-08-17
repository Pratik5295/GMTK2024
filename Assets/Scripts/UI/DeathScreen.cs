using TMPro;
using UnityEngine;
using static MetaConstants;

[DefaultExecutionOrder(3)]
public class DeathScreen : UIScreen
{
    public static DeathScreen Instance = null;

    [SerializeField] private TextMeshProUGUI deathText;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private DeathCodes deathCode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Start()
    {
        base.Start();

        gameManager = GameManager.Instance;

        if(gameManager != null)
        {
            gameManager.OnStateChanged += OnGameStateChangedHandler;
        }
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnStateChanged -= OnGameStateChangedHandler;
        }
    }

    private void OnGameStateChangedHandler(GameState state)
    {
        switch (state)
        {
            case GameState.GAMEOVER:
                PopulateDeathScreen();
                break;
        }
    }

    /// <summary>
    /// Set this code prior to changing the game mode to game over
    /// </summary>
    /// <param name="_code"></param>
    public void SetDeathCode(DeathCodes _code)
    {
        deathCode = _code;
    }

    private void PopulateDeathScreen()
    {
        //Custom death message
        string deathMessage = DeathMessages.GetDeathMessage(deathCode);
        deathText.text = deathMessage; 

        Show();
    }


}
