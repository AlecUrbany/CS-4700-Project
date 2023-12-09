using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SpawnSide{
    Left,
    Right,
    Behind,
    Front
}
public class LoadLevel : MonoBehaviour
{
    public ScreenFader fader;
    public static bool isPaused;
    float previousTimeScale = 1;
    public string targetSceneName; // Name of scene we want to switch to
    public string targetPortal; //Name of Portal to teleport to
    public SpawnSide thisPortalsSpawnSide;
    public float thisPortalsSpawnOffset = 3;
    public float thisPortalsHortizontalOffset = 0;
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }

        //PS1 style fade to black. Will need to implement method to freeze the player later on.
        float timer = 2;
        EventBus.Instance.LevelLoadStart();
        StartCoroutine(Delay(timer));

    }

    
    //Actual move to next level. Will need to make prope code later.
    private IEnumerator Delay(float duration)
    {
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        PlayerPrefs.SetString("TargetSpawnPoint", targetPortal);
        Debug.Log("Entering next level");
        fader.FadeFromBlack(duration);
        EventBus.Instance.LevelLoadEnd();
        SceneManager.LoadScene(targetSceneName);
    }
}
