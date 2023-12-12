using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.InteropServices;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera  targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        primaryCamera = GameObject.FindWithTag("PlayerFollowCam").GetComponent<CinemachineVirtualCamera>();
        targetCamera.Priority = 9;
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
    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        targetCamera.enabled = true;
        targetCamera.Priority = 11;
    }
    private void SwitchBackToCamera(CinemachineVirtualCamera targetCamera)
    {
        targetCamera.enabled = false;
        targetCamera.Priority = 9;
    }
}
