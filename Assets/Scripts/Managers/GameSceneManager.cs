using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    MENU = 0,
    GAME = 1
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance = null;

    [SerializeField] private SceneEnum activeScene;

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

    public void LoadGameScene(SceneEnum _scene)
    {
        activeScene = _scene;
        SceneManager.LoadScene((int) activeScene);
    }

}
