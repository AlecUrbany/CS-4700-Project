using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodecTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip callSound;
    public GameObject codecButton;
    
    /*[SerializeField] private Image Image;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image.enabled = false;
        }
    }*/
    public DialogueTrigger trigger;

    private void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
            trigger.StartDialogue();
            source.PlayOneShot(callSound, 1f);
    }
    public void Start()
    {
        source = GameObject.FindWithTag("CodecFunction").GetComponent<AudioSource>();
    }
}
