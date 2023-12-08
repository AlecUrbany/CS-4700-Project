using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    public bool inAlertPhase;
    public double timeRemaining = 0;
    private double alertDuration = 5;

    public void updateCanSeePlayer(bool canSeePlayer) 
    {
        if (canSeePlayer) 
        {
            // if (inAlertPhase == false) {
            //     // We are entering alert phase
            //     // TODO: Add Enemy spawn and other stuff that happens when first entering alert phase
            // }

            inAlertPhase = true;
            miniMap.SetActive(false);
            AlertInfo.SetActive(true);
            // Maybe set timeRemaining with a constant variable (useful for multiple game difficulties)
            timeRemaining = Math.Max(timeRemaining, alertDuration);
            TimerText.text = string.Format("{0:00}", timeRemaining);
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
