using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame ()
    {
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(1);
    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    
}
