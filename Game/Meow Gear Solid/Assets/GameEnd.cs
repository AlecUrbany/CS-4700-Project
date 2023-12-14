using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    public bool hasMacguffin;
    GameObject exitTruck;
    GameObject exitText;
    public ScreenFader fader;
    // Start is called before the first frame update
    void Start()
    {
        exitTruck = GetComponent<GameObject>();
        hasMacguffin = EventBus.Instance.hasMacguffin;
        exitTruck.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasMacguffin == true)
        {
           exitTruck.SetActive(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            exitText.SetActive(false);
            return;
        }

        exitText.SetActive(true);
        if (Input.GetButton("Interact"))
        {
            float timer = 1;
            EventBus.Instance.LevelLoadStart();
            StartCoroutine(Delay(timer));
        }
    }
    private IEnumerator Delay(float duration)
    {
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        Debug.Log("Entering final level");
        fader.FadeFromBlack(duration);
        SceneManager.LoadScene("Ending");
    }
}
