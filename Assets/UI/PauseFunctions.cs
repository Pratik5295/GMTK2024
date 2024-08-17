using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseFunctions : MonoBehaviour
{
    [SerializeField] int mainLevelSceneIndex;
    public bool enableInput;

    private void Start()
    {
        enableInput = true;
    }


    public void PressRetryButton()
    {
        if (enableInput)
        {
            //SceneManager.LoadScene(mainLevelSceneIndex);//this should set to the current scene name instead
            Debug.Log("retry");
        }
    }

    
    public void PressOptionsButton()
    {
        if (enableInput)
        {
            PauseMenu.instance.ToggleOptions();//this should only be able to be pressed when the options button is visible
            Debug.Log("options");
        }
    }
    

    public void PressQuitButton()
    {
        if (enableInput)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }

    public void ButtonSound(AudioSource audio)
    {
        if (enableInput)
        {
            audio.Play();
        }
    }
}
