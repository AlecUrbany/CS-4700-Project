using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCodecTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip callSound;
    public AudioClip pickUpSound;
    public GameObject callButton;
    
    public bool isCalling;

    public bool hasCalled;
    public DialogueTrigger trigger;
    public void Update()
    {
        if(EventBus.Instance.hasMacguffin == true)
        {
            callButton.SetActive(true);
            hasCalled = true;
            isCalling = true;
            callButton.SetActive(true);
            StartCoroutine("Timeout");
        }
    }
    public void Start()
    {
        source = GameObject.FindWithTag("CodecFunction").GetComponent<AudioSource>();
        callButton.SetActive(false);
        hasCalled = false;
        isCalling = false;

    }
    IEnumerator Timeout()
    {
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(.5f);
        source.PlayOneShot(pickUpSound, 1f);
        trigger.StartDialogue();
        hasCalled = false;
        isCalling = false;
    }
}
