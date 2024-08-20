using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum SceneEnum
{
    MENU = 0,
    GAME = 1
}

[DefaultExecutionOrder(1)]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance = null;

    [SerializeField] private SceneEnum activeScene;

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            activeScene = SceneEnum.MENU;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        if(gameManager == null)
        {
            Debug.LogWarning("Missing Game Manager");
        }
        else
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

    public void LoadGameScene(SceneEnum _scene)
    {
        activeScene = _scene;
        SceneManager.LoadScene((int) activeScene);
    }

    public void RestartLevel()
    {
        GameManager.Instance.ResetScore();
        Scene gameScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(gameScene.name);
    }

    private void OnGameStateChangedHandler(GameState _state)
    {
        switch(_state)
        {
            case GameState.DEFAULT:
                //Open the main menu up
                LoadGameScene(SceneEnum.MENU);
                break;

            case GameState.GAME:

                //Start the game scene only if its a new game
                if(!gameManager.IsPaused)
                {
                    LoadGameScene(SceneEnum.GAME);
                }
                break;

            default:
                break;
        }
    }


}
