using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.InteropServices;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject primaryCamera;
    public GameObject targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        primaryCamera = GameObject.FindWithTag("PlayerFollowCam").GetComponent<GameObject>();
        targetCamera.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
            if(other.CompareTag("Player"))
            { 
                SwitchToCamera(targetCamera);
            }
    }
    private void OnTriggerExit(Collider other)
    {
            if(other.CompareTag("Player"))
            {
                SwitchBackToCamera(targetCamera);
            }
    }
    private void SwitchToCamera(GameObject targetCamera)
    {
        targetCamera.SetActive(true);
        primaryCamera.SetActive(false);
    }
    private void SwitchBackToCamera(GameObject targetCamera)
    {
        targetCamera.SetActive(false);
        primaryCamera.SetActive(true);
    }
}
