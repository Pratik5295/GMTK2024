using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance = null;

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

    public void OnGameStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnGameSettingsButtonClicked()
    {
        //Do nothing for now
    }


    public void OnGameExitButtonClicked()
    {
        //Quit game
        Application.Quit();
    }
}
