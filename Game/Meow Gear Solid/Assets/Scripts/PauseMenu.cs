using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    float previousTimeScale = 1;
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        //Takes pause button input
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if(Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
            pauseMenu.SetActive(true);
            EventBus.Instance.OpenInventory();
        }
        else if (Time.timeScale == 0)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
            EventBus.Instance.CloseInventory();
        }
    }
}

