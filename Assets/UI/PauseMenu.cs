using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject pauseScreen, optionsScreen;
    public bool disablePauseMenu, isPaused, isOnOptions;

    public static Action OnPause;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (disablePauseMenu)
        {
            pauseScreen.SetActive(false);
            isPaused = false;
        }
        else
        {
            pauseScreen.SetActive(true);
            isPaused = true;
        }
        optionsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOnOptions)
            {
                TogglePause();
            }
            else
            {
                ToggleOptions();
            }
            
        }
        
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            isPaused = false;
            //GameManager.instance.ToggleControl(true);
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseScreen.SetActive(true);
            isPaused = true;
            //GameManager.instance.ToggleControl(false);
            OnPause?.Invoke();
        }
    }

    
    public void ToggleOptions()
    {
        if (isOnOptions)
        {
            pauseScreen.SetActive(true);
            optionsScreen.SetActive(false);
            isOnOptions = false;
        }
        else
        {
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(true);
            isOnOptions = true;
        }
    }
}
