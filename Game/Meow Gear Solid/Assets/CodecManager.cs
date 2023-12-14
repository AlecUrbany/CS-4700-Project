using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CodecManager : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public Button callButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Codec"))
        {
            dialogueTrigger = GameObject.FindGameObjectWithTag("CodecTrigger").GetComponent<DialogueTrigger>();
        }
    }
}
