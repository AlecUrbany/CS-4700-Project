using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public ScreenFader fader;
    public static bool isPaused;
    float previousTimeScale = 1;
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }

        //PS1 style fade to black. Will need to implement method to freeze the player later on.
        float timer = 2;
        fader.FadeToBlack(timer);
        StartCoroutine(Delay(timer));

    }

    void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            Debug.Log("PAUSING");
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
            previousTimeScale = Time.timeScale;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
        }
    }
    
    //Actual move to next level. Will need to make prope code later.
    private IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Entering next level");
        fader.FadeFromBlack(duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
