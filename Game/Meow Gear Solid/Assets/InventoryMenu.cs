using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryMenu;
    float previousTimeScale = 1;
    public static bool isPaused;
    public static bool inventoryOpen;
    void Start()
    {
        inventoryMenu.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Toggle Inventory"))
        {
            TogglePause();
        }
                
    }
    void TogglePause()
    {
        if(Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
            inventoryMenu.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            inventoryMenu.SetActive(false);
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
        }
    }
}
