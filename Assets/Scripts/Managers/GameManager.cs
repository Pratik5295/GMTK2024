using System;
using UnityEngine;

public enum GameState
{
    DEFAULT = 0,        //Empty state
    GAME = 1,
    PAUSED = 2,
    GAMEOVER = 3
}

[DefaultExecutionOrder(0)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Game State")]
    [SerializeField] private GameState state;
    [SerializeField] private bool isPaused = false;

    public Action<GameState> OnStateChanged;
    public GameState State => state;
    public bool IsPaused => isPaused;

    [SerializeField] private PlayerHealth player;

    //Score system section
    [SerializeField] private int score;
    public int Score => score;
    public Action<int> OnScoreChangeEvent;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            state = GameState.DEFAULT;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void SetState(GameState _state)
    {
        state = _state;
        OnStateChanged?.Invoke(state);
    }

    public void StartGame()
    {
        SetState(GameState.GAME);
    }

    public void GameOver()
    {
        //Will be fired when the player has died
        SetState(GameState.GAMEOVER);

        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        SetState(GameState.PAUSED);
        isPaused = true;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SetState(GameState.GAME);
        isPaused = false;

        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        SetState(GameState.DEFAULT);
    }

    public void SetPlayer(PlayerHealth _player)
    {
        player = _player;
        player.OnPlayerDeathEvent += OnPlayerDeathEventHandler;
    }

    private void OnPlayerDeathEventHandler()
    {
        player.OnPlayerDeathEvent -= OnPlayerDeathEventHandler;

        GameOver();
    }


    #region Score System Section

    public void AddScore(int _amount)
    {
        score += _amount;
        OnScoreChangeEvent?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
    }

    #endregion
}
