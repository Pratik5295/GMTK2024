using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(IStringDefinitions.TESTING_SCENE);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
