using UnityEngine;

public class PauseMenu : UIScreen
{
    public static PauseMenu Instance = null;

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

    private void Update()
    {
        //Only for testing, will be moved later
        if(!isActive)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }
        }
        
    }

    public void OnGameExitButtonClicked()
    {
        //Quit game
        Application.Quit();
    }

    public void OnGameQuitToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();

        Show();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();

        Hide();
    }
}
