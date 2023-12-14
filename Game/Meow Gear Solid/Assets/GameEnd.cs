using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip endingCutscene;
    public GameObject exitTruck;
    public GameObject exitText;
    public bool hasGuffin;
    public ScreenFader fader;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        exitTruck.SetActive(false);
        hasGuffin = EventBus.Instance.hasMacguffin;
        player = GameObject.FindWithTag("Player").GetComponent<GameObject>();
        exitText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(EventBus.Instance.hasMacguffin == true)
        {
           exitTruck.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        exitText.SetActive(false);
        if( EventBus.Instance.hasMacguffin == true);
        {            

            exitText.SetActive(true);
            if (Input.GetButtonDown("Interact") && EventBus.Instance.hasMacguffin == true)
            {
                EventBus.Instance.LevelLoadStart();
                EventBus.Instance.GameEnd();
                StartCoroutine("Delay");
            }
        }
    }
    private IEnumerator Delay()
    {
        fader.FadeToBlack(1f);
        yield return new WaitForSeconds(1f);
        StartCoroutine("CutsceneDelay");
        Debug.Log("Fade was a success");
    }
    private IEnumerator CutsceneDelay()
    {
        Debug.Log("Cutscene was a success");
        audioSource.PlayOneShot(endingCutscene, 1f);
        yield return new WaitForSeconds(10);
        Debug.Log("Entering final level");
        SceneManager.LoadScene("Ending");
    }
}
