using TMPro;
using UnityEngine;

[DefaultExecutionOrder(3)]
public class DeathScreen : UIScreen
{
    public static DeathScreen Instance = null;

    [SerializeField] private TextMeshProUGUI deathText;

    [SerializeField] private GameManager gameManager;

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

    private void PopulateDeathScreen()
    {
        //Custom death message

        Show();
    }


}
