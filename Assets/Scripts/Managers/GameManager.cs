using System;
using UnityEngine;

public enum GameState
{
    DEFAULT = 0,
    GAME = 1,
    PAUSED = 2,
    GAMEOVER = 3
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Game State")]
    [SerializeField] private GameState state;

    public Action<GameState> OnStateChanged;
    public GameState State => state;

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

    public void PauseGame()
    {
        SetState(GameState.PAUSED);
    }

    public void ResumeGame()
    {
        SetState(GameState.GAME);
    }
}
