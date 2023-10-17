using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    private bool inAlertPhase;
    public float timeRemaining = 0;

    public void updateCanSeePlayer(bool canSeePlayer) 
    {
        if (canSeePlayer) 
        {
            inAlertPhase = true;
            miniMap.SetActive(false);
            AlertInfo.SetActive(true);
            // Maybe set timeRemaining with a constant variable (useful for multiple game difficulties)
            timeRemaining = 5;
            TimerText.text = string.Format("{0:00}", timeRemaining);
            // TimerText.SetText(string.Format("{0:00}", timeRemaining));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inAlertPhase) 
        {
            if (timeRemaining > 0) 
            {
                timeRemaining -= Time.deltaTime;
                TimerText.text = string.Format("{0:00}", timeRemaining);
                // TimerText.SetText(string.Format("{0:00}", timeRemaining));
                // TODO: DO Stuff!
            }
            else
            {
                timeRemaining = 0;
                inAlertPhase = false;
                miniMap.SetActive(true);
                AlertInfo.SetActive(false);
            }
        }
    }
}
