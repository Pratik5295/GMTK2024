using UnityEngine;

[DefaultExecutionOrder(3)]
public class CursorHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if(gameManager != null)
        {
            gameManager.OnStateChanged += OnGameStateChangedHandler;
        }

        OnGameStateChangedHandler(gameManager.State);
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnStateChanged -= OnGameStateChangedHandler;
        }
    }

    private void OnGameStateChangedHandler(GameState currState) 
    {
        switch (currState)
        {
            case GameState.PAUSED:
                UnlockCursor();
                break;

            case GameState.GAME:
                LockCursor();
                break;
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
