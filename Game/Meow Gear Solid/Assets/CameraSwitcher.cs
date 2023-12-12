using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.InteropServices;

public class CameraSwitcher : MonoBehaviour
{
    public string triggerTag;
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera[] virtualCameras;
    // Start is called before the first frame update
    void Start()
    {
        primaryCamera = GameObject.FindWithTag("PlayerFollowCam").GetComponent<CinemachineVirtualCamera>();
        SwitchToCamera(primaryCamera);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
                SwitchToCamera(targetCamera);
            }
    }
    private void OnTriggerExit(Collider other)
    {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SwitchToCamera(primaryCamera);
            }
    }
    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach(CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }
}
