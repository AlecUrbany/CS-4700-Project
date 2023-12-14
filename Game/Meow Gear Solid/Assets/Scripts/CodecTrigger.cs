using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodecTrigger : MonoBehaviour
{
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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") == true)
            trigger.StartDialogue();
    }
}
