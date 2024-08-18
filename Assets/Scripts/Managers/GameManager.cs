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
}